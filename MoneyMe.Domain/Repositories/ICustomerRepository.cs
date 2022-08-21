using MoneyMe.Domain.CustomerAggregate;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task<Customer> GetCustomerByName();
    }
}