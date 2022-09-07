using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerDto> FindCustomerByEmailAsync(string email);
        Task<CustomerDto> GetCustomerAsync(Guid customerId);
        Task<CustomerDto> RegisterCustomerAsync(CustomerDto customerDto);
        Task<CustomerDto> UpdateCustomerAsync(CustomerDto customerDto);
    }
}