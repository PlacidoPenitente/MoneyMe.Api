using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
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

        public CustomerService(ICustomerFactory customerFactory, ICustomerRepository customerRepository)
        {
            _customerFactory = customerFactory;
            _customerRepository = customerRepository;
        }

        public CustomerDto FindCustomerByEmail(string email)
        {
            return new CustomerDto();
        }

        public async Task RegisterCustomer(CustomerDto customerDto)
        {
            var customer = _customerFactory.Create(
                customerDto.Title,
                customerDto.FirstName,
                customerDto.LastName,
                customerDto.DateOfBirth,
                customerDto.Mobile,
                customerDto.Email);

            await _customerRepository.Add(customer);

            customerDto.Id = customer.Id;
        }

        Task<CustomerDto> ICustomerService.RegisterCustomer(CustomerDto customerDto)
        {
            throw new NotImplementedException();
        }
    }
}