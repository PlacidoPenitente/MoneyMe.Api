using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.Repositories;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IQuoteFactory _quoteFactory;
        private readonly IProductRepository _productRepository;

        public QuoteService(
            IQuoteRepository quoteRepository,
            IQuoteFactory quoteFactory,
            IProductRepository productRepository)
        {
            _quoteRepository = quoteRepository;
            _quoteFactory = quoteFactory;
            _productRepository = productRepository;
        }

        public async Task<QuoteDto> CalculateAsync(PartialQuoteDto partialQuoteDto)
        {
            var product = await _productRepository.FindByNumberOfTerms(partialQuoteDto.Terms);

            var quote = _quoteFactory.Create(partialQuoteDto.CustomerId, partialQuoteDto.AmountRequired);

            quote.ApplyProductTerms(product);

            await _quoteRepository.AddAsync(quote);

            return new QuoteDto
            {
                Id = quote.Id,
                DateAdded = quote.DateAdded,
                DateModified = quote.DateModified,
                CustomerId = quote.CustomerId,
                LoanAmount = quote.LoanAmount,
                Terms = quote.Terms,
                MonthlyPayment = quote.MonthlyPayment
            };
        }
    }
}