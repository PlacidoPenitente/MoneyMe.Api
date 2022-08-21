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

        public QuoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Quote quote)
        {
            await _context.AddAsync(quote);
        }

        public async Task<Quote> GetAsync(Guid id)
        {
            return await _context.Quotes.SingleOrDefaultAsync(quote => quote.Id == id);
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == null) return;
            var quoteToBeRemoved = await GetAsync(id);

            _context.Remove(quoteToBeRemoved);
        }
    }
}