using AutoMapper;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
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
        private readonly IMapper _mapper;

        public CustomerService(ICustomerFactory customerFactory, ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerFactory = customerFactory;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDto> FindCustomerByEmailAsync(string email)
        {
            var customer = await _customerRepository.FindByEmailAsync(email);

            if (customer == null) return null;

            return new CustomerDto(customer.Id)
            {
                DateAdded = customer.DateCreated,
                DateModified = customer.DateModified,
                Title = customer.Title.ToString(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                MobileNumber = customer.MobileNumber,
                EmailAddress = customer.EmailAddress
            };
        }

        public async Task<CustomerDto> GetCustomerAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetAsync(customerId);

            return new CustomerDto(customer.Id)
            {
                DateAdded = customer.DateCreated,
                DateModified = customer.DateModified,
                Title = customer.ToString(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                MobileNumber = customer.MobileNumber,
                EmailAddress = customer.EmailAddress
            };
        }

        public async Task<CustomerDto> RegisterCustomerAsync(CustomerDto customerDto)
        {
            var customer = _customerFactory.Create(
                customerDto.Title,
                customerDto.FirstName,
                customerDto.LastName,
                customerDto.DateOfBirth,
                customerDto.MobileNumber,
                customerDto.EmailAddress);


            await using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () => await _customerRepository.AddAsync(customer));
            };

            return new CustomerDto(customer.Id)
            {
                DateAdded = customer.DateCreated,
                DateModified = customer.DateModified.Value,
                Title = customer.Title.ToString(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                MobileNumber = customer.MobileNumber,
                EmailAddress = customer.EmailAddress
            };
        }

        public async Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto)
        {
            await using (_unitOfWork)
            {
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    var customer = await _customerRepository.GetAsync(customerDto.Id);

                    customer.ChangeTitle(customerDto.Title);
                    customer.ChangeFirstName(customerDto.FirstName);
                    customer.ChangeLastName(customerDto.LastName);
                    customer.ChangeDateOfBirth(customerDto.DateOfBirth);
                    customer.ChangeMobileNumber(customerDto.MobileNumber);
                    customer.ChangeEmailAddress(customerDto.EmailAddress);

                    _customerRepository.Update(customer);

                    return customerDto;
                });
            };
        }
    }
}