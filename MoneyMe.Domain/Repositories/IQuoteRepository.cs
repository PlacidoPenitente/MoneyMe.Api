using MoneyMe.Domain.QuoteAggregate;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface IQuoteRepository
    {
        Task AddAsync(Quote customer);
        Task<Quote> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
    }
}