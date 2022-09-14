using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IQuoteService
    {
        Task<QuoteDto> CalculateAsync(PartialQuoteDto partialQuoteDto);
        Task<QuoteDto> ReadQuoteAsync(Guid id);
        Task<IEnumerable<QuoteDto>> ReadAllQuotesAsync();
        Task<QuoteDto> UpdateQuoteAsync(QuoteDto quoteDto);
        Task DeleteQuoteAsync(Guid id);
    }
}