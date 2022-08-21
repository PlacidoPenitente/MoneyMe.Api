using MoneyMe.Application.Contracts.Dtos;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerDto> FindCustomerByEmail(string email);
        Task RegisterCustomer(CustomerDto customerDto);
    }
}