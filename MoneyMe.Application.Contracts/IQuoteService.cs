using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IQuoteService
    {
        public Task<string> GenerateQuoteRedirectUrl(PartialQuoteDto quoteDto);
        public Task<PartialQuoteDto> ContinueQuoteAsync(Guid quoteId);
        Task<PartialQuoteDto> CalculateAsync(Guid quoteId, Guid productId);
    }
}