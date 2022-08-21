using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IQuoteService
    {
        Task<QuoteDto> CalculateAsync(PartialQuoteDto partialQuoteDto);
    }
}