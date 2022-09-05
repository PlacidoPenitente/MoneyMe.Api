using AutoMapper;
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
        private readonly IMapper _mapper;

        public CustomerRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.AddAsync(customer);
        }

        public async Task<Customer> GetAsync(Guid id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Customer>(customer);
        }

        public async Task<Customer> FindByEmailAsync(string emailAddress)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(customer => customer.EmailAddress == emailAddress);
            return _mapper.Map<Customer>(customer);
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == null) return;
            var customerToBeRemoved = await GetAsync(id);

            _context.Remove(customerToBeRemoved);
        }
    }
}