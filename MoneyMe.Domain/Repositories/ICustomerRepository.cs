using MoneyMe.Domain.CustomerAggregate;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer> GetAsync(Guid id);
        Task<Customer> FindByEmailAsync(string email);
        Task RemoveAsync(Guid id);
    }
}