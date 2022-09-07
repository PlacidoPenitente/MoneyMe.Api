using AutoMapper;
using MoneyMe.Domain.FeeAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;

namespace MoneyMe.Infrastructure.Repositories
{
    public sealed class FeeRepository : Repository<Fee, Database.Models.Fee>, IFeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FeeRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
