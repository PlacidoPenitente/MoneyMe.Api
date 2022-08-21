using MoneyMe.Domain.QuoteAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface IQuoteRepository
    {
        Task AddAsync(Quote quote);
        Task<Quote> GetAsync(Guid quoteId);
    }
}