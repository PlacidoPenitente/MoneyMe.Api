using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain.Factories;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class QuoteService : IQuoteService
    {
        public Task<string> RegisterQuote(QuoteRequestDto quoteDto)
        {
            return null;
        }

        public Task<string> RequestQuote(Guid quoteId)
        {
            throw new NotImplementedException();
        }
    }
}