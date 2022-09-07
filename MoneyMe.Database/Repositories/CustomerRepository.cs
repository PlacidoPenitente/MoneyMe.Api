using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Repositories
{
    public sealed class CustomerRepository : Repository<Customer, Database.Models.Customer>,   ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Customer> FindByEmailAsync(string emailAddress)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(customer => customer.EmailAddress == emailAddress);
            return _mapper.Map<Customer>(customer);
        }
    }
}