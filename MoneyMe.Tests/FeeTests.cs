using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MoneyMe.Application;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.FeeAggregate;
using MoneyMe.Domain.Repositories;
using Moq;
using Xunit;

namespace MoneyMe.Tests
{
    [Collection("Fee")]
    public class FeeTests
    {
        public FeeService Sut { get; }
        public Mock<IFeeFactory> FeeFactory { get; }
        public Mock<IFeeRepository> FeeRepository { get; }
        public Mock<IUnitOfWork> UnitOfWork { get; }
        public List<Fee> Fees { get; } = new List<Fee>();

        public FeeTests()
        {
            FeeFactory = new Mock<IFeeFactory>();
            FeeRepository = new Mock<IFeeRepository>();
            UnitOfWork = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationMappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();

            Sut = new FeeService(FeeFactory.Object, FeeRepository.Object, UnitOfWork.Object, mapper);

            Setup();
        }

        [Fact]
        public async Task Should_create_and_return_fee()
        {
            var newFee = new FeeDto
            {
                Name = "Generic Fee",
                Amount = 100,
                IsPercentage = false
            };

            var addedFee = await Sut.CreateFeeAsync(newFee);
            var retrievedFee = await Sut.ReadFeeAsync(addedFee.Id.Value);

            FeeFactory.Verify(x => x.Create(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<bool>()), Times.Once);
            FeeRepository.Verify(x => x.AddAsync(It.IsAny<Fee>()), Times.Once);
            UnitOfWork.Verify(x => x.ExecuteAsync(It.IsAny<Func<Task>>()), Times.Once);

            retrievedFee.Should().NotBeNull();
            retrievedFee.Id.Should().Be(addedFee.Id);
            retrievedFee.Name.Should().Be(newFee.Name);
            retrievedFee.Amount.Should().Be(newFee.Amount);
            retrievedFee.IsPercentage.Should().Be(newFee.IsPercentage);
            retrievedFee.DateCreated.Should().Be(addedFee.DateCreated);
            retrievedFee.DateModified.Should().BeNull();
        }

        [Fact]
        public async Task Should_create_and_return_all_fees()
        {
            var firstFee = new FeeDto
            {
                Name = "First Fee",
                Amount = 100,
                IsPercentage = false
            };

            var secondFee = new FeeDto
            {
                Name = "Second Fee",
                Amount = 200,
                IsPercentage = true
            };

            var addedFirstFee = await Sut.CreateFeeAsync(firstFee);
            var addedSecondFee = await Sut.CreateFeeAsync(secondFee);

            var retrievedFees = (await Sut.ReadAllFeesAsync()).ToList();

            FeeFactory.Verify(x => x.Create(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<bool>()), Times.Exactly(2));
            FeeRepository.Verify(x => x.AddAsync(It.IsAny<Fee>()), Times.Exactly(2));
            FeeRepository.Verify(x => x.GetAllAsync(), Times.Once);
            UnitOfWork.Verify(x => x.ExecuteAsync(It.IsAny<Func<Task>>()), Times.Exactly(2));

            retrievedFees.Should().NotBeNull();

            retrievedFees.Count.Should().Be(2);

            retrievedFees.Should().Contain(
                x => x.Id == addedFirstFee.Id &&
                     x.Name == firstFee.Name &&
                     x.Amount == firstFee.Amount &&
                     x.IsPercentage == firstFee.IsPercentage &&
                     x.DateCreated == addedFirstFee.DateCreated &&
                     x.DateModified == null);

            retrievedFees.Should().Contain(
                x => x.Id == addedSecondFee.Id &&
                     x.Name == secondFee.Name &&
                     x.Amount == secondFee.Amount &&
                     x.IsPercentage == secondFee.IsPercentage &&
                     x.DateCreated == addedSecondFee.DateCreated &&
                     x.DateModified == null);
        }

        [Fact]
        public async Task Should_create_and_update_fee()
        {
            var newFee = new FeeDto
            {
                Name = "Generic Fee",
                Amount = 100,
                IsPercentage = false
            };

            var addedFee = await Sut.CreateFeeAsync(newFee);

            addedFee.Name = "New Fee";
            addedFee.Amount = 200;
            addedFee.IsPercentage = false;

            var updatedFee = await Sut.UpdateFeeAsync(addedFee);

            FeeRepository.Verify(x => x.AddAsync(It.IsAny<Fee>()), Times.Once);
            FeeRepository.Verify(x => x.Update(It.IsAny<Fee>()), Times.Once);
            UnitOfWork.Verify(x => x.ExecuteAsync(It.IsAny<Func<Task<FeeDto>>>()), Times.Once);
            UnitOfWork.Verify(x => x.ExecuteAsync(It.IsAny<Func<Task>>()), Times.Once);

            updatedFee.Should().NotBeNull();
            updatedFee.Id.Should().Be(addedFee.Id);
            updatedFee.Name.Should().Be(addedFee.Name);
            updatedFee.Amount.Should().Be(addedFee.Amount);
            updatedFee.IsPercentage.Should().Be(addedFee.IsPercentage);
            updatedFee.DateCreated.Should().Be(addedFee.DateCreated);
            updatedFee.DateModified.Should().NotBeNull();
        }

        [Fact]
        public async Task Should_create_and_delete_fee()
        {
            var newFee = new FeeDto
            {
                Name = "Generic Fee",
                Amount = 100,
                IsPercentage = false
            };

            var addedFee = await Sut.CreateFeeAsync(newFee);
            await Sut.DeleteFeeAsync(addedFee.Id.Value);
            addedFee = await Sut.ReadFeeAsync(addedFee.Id.Value);

            FeeFactory.Verify(x => x.Create(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<bool>()), Times.Once);
            FeeRepository.Verify(x => x.AddAsync(It.IsAny<Fee>()), Times.Once);
            FeeRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Exactly(2));
            FeeRepository.Verify(x => x.Remove(It.IsAny<Fee>()), Times.Once);
            UnitOfWork.Verify(x => x.ExecuteAsync(It.IsAny<Func<Task>>()), Times.Exactly(2));

            addedFee.Should().BeNull();
        }

        private void Setup()
        {
            FeeFactory.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<bool>()))
                .Returns((string name, decimal amount, bool isPercentage) =>
                {
                    return new Fee(Guid.NewGuid(), DateTime.UtcNow, null, name.Trim(), amount, isPercentage);
                });

            FeeRepository.Setup(x => x.AddAsync(It.IsAny<Fee>())).Callback((Fee fee) => Fees.Add(fee));
            FeeRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => Fees.SingleOrDefault(c => c.Id == id));
            FeeRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(Fees);

            FeeRepository.Setup(x => x.Update(It.IsAny<Fee>())).Callback((Fee updatedFee) =>
            {
                var existingFee = Fees.SingleOrDefault(c => c.Id == updatedFee.Id);

                existingFee.ChangeName(updatedFee.Name);
                existingFee.ChangeAmount(updatedFee.Amount);

                if (updatedFee.IsPercentage)
                {
                    updatedFee.SetAsPercentage();
                }
                else
                {
                    updatedFee.SetAsFixedAmount();
                }
            });

            FeeRepository.Setup(x => x.Remove(It.IsAny<Fee>())).Callback((Fee fee) => Fees.Remove(fee));

            UnitOfWork.Setup(x => x.ExecuteAsync(It.IsAny<Func<Task>>())).Returns((Func<Task> task) => task());

            UnitOfWork.Setup(x => x.ExecuteAsync(It.IsAny<Func<Task<FeeDto>>>())).Returns((Func<Task<FeeDto>> task) => task());
        }
    }
}