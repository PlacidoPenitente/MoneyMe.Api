using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Infrastructure.Database;
using AutoMapper;
using MoneyMe.Domain.Repositories;

namespace MoneyMe.Infrastructure.Repositories
{
    public sealed class LoanRepository : Repository<Loan, Database.Models.Loan>, ILoanRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LoanRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}