using MoneyMe.Domain.QuoteAggregate;
using MoneyMe.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        public Task AddAsync(Quote quote)
        {
            return Task.CompletedTask;
        }

        public async Task<Quote> GetAsync(Guid quoteId)
        {
            var quote = new Quote(quoteId, DateTime.UtcNow, DateTime.UtcNow, Guid.NewGuid(), 5000);

            return await Task.FromResult(quote);
        }
    }
}