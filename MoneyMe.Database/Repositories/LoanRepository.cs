using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MoneyMe.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LoanRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Loan loan)
        {
            await _context.AddAsync(loan);
        }

        public async Task<Loan> FindByCustomerIdAsync(Guid customerId)
        {
            var loan = await _context.Loans.SingleOrDefaultAsync(loan => loan.CustomerId == customerId);
            return _mapper.Map<Loan>(loan);
        }

        public async Task<Loan> GetAsync(Guid id)
        {
            var loan = await _context.Loans.SingleOrDefaultAsync(loan => loan.Id == id);
            return _mapper.Map<Loan>(loan);
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == null) return;
            var loanToBeRemoved = await GetAsync(id);

            _context.Remove(loanToBeRemoved);
        }
    }
}