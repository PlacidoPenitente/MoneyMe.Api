using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.Repositories;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task Add(Customer customer)
        {
            return Task.FromResult(customer);
        }

        public Task<Customer> GetCustomerByName()
        {
            throw new System.NotImplementedException();
        }
    }
}