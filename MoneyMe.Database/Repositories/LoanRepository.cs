using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;
using System.Threading.Tasks;
using System;
using MoneyMe.Domain.ApplicationAggregate;
using Microsoft.EntityFrameworkCore;

namespace MoneyMe.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Loan loan)
        {
            await _context.AddAsync(loan);
        }

        public async Task<Loan> FindByCustomerIdAsync(Guid customerId)
        {
            return await _context.Loans.SingleOrDefaultAsync(loan => loan.CustomerId == customerId);
        }

        public async Task<Loan> GetAsync(Guid id)
        {
            return await _context.Loans.SingleOrDefaultAsync(loan => loan.Id == id);
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == null) return;
            var loanToBeRemoved = await GetAsync(id);

            _context.Remove(loanToBeRemoved);
        }
    }
}