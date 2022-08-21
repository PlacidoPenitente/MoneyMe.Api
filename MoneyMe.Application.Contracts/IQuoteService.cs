using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IQuoteService
    {
        public Task<string> RequestQuoteAsync(QuoteDto quoteDto);
        public Task<QuoteDto> ContinueQuoteAsync(Guid quoteId);
        Task<QuoteDto> CalculateAsync(Guid quoteId, Guid productId);
    }
}