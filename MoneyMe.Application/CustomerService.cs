using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.QuoteAggregate;
using MoneyMe.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerFactory _customerFactory;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerFactory customerFactory, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerFactory = customerFactory;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> FindCustomerByEmailAsync(string email)
        {
            var customer = await _customerRepository.FindByEmailAsync(email);

            return new CustomerDto
            {
                Id = customer.Id,
                DateAdded = customer.DateAdded,
                DateModified = customer.DateModified,
                Title = customer.Title,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                Mobile = customer.MobileNumber,
                Email = customer.EmailAddress
            };
        }

        public async Task<CustomerDto> GetCustomerAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);

            return new CustomerDto
            {
                Id = customer.Id,
                DateAdded = customer.DateAdded,
                DateModified = customer.DateModified,
                Title = customer.Title,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                Mobile = customer.MobileNumber,
                Email = customer.EmailAddress
            };
        }

        public async Task RegisterCustomerAsync(CustomerDto customerDto)
        {
            var customer = _customerFactory.Create(
                customerDto.Title,
                customerDto.FirstName,
                customerDto.LastName,
                customerDto.DateOfBirth,
                customerDto.Mobile,
                customerDto.Email);


            await using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () => await _customerRepository.AddAsync(customer));
            };

            customerDto.Id = customer.Id;
        }
    }
}