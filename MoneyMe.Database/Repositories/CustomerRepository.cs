using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.AddAsync(customer);
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> FindByEmailAsync(string emailAddress)
        {
            return await _context.Customers.SingleOrDefaultAsync(customer => customer.EmailAddress == emailAddress);
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == null) return;
            var customerToBeRemoved = await GetAsync(id);

            _context.Remove(customerToBeRemoved);
        }
    }
}