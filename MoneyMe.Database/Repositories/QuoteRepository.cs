using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.QuoteAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public QuoteRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Quote quote)
        {
            await _context.AddAsync(quote);
        }

        public async Task<Quote> GetAsync(Guid id)
        {
            var quote = await _context.Quotes.SingleOrDefaultAsync(quote => quote.Id == id);
            return _mapper.Map<Quote>(quote);
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == null) return;
            var quoteToBeRemoved = await GetAsync(id);

            _context.Remove(quoteToBeRemoved);
        }
    }
}