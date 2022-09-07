using MoneyMe.Domain.CustomerAggregate;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> FindByEmailAsync(string emailAddress);
    }
}