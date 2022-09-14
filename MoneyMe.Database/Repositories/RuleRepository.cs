using AutoMapper;
using MoneyMe.Domain.Repositories;
using MoneyMe.Domain.RuleAggregate;
using MoneyMe.Infrastructure.Database;

namespace MoneyMe.Infrastructure.Repositories
{
    public class RuleRepository : Repository<Rule, Database.Models.Rule>, IRuleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RuleRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}