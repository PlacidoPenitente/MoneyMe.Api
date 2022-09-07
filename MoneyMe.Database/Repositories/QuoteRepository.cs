using AutoMapper;
using MoneyMe.Domain.QuoteAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;

namespace MoneyMe.Infrastructure.Repositories
{
    public sealed class QuoteRepository : Repository<Quote, Database.Models.Quote>, IQuoteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public QuoteRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}