using MoneyMe.Domain.CustomerAggregate;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface ICustomerRepository
    {
        public Task<Customer> GetCustomerByName();
    }
}