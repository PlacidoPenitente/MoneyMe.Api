using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain.CustomerAggregate;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerDto> RegisterCustomer(CustomerDto customerDto);
    }
}