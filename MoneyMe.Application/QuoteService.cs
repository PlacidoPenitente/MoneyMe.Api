using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain.Factories;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class QuoteService : IQuoteService
    {
        public QuoteService(CustomerFactory customerFactory)
        {

        }

        public Task<string> RequestQuote(QuoteRequestDto quoteDto)
        {

        }

        public Task<string> RequestQuote(Guid quoteId)
        {
            throw new NotImplementedException();
        }
    }
}