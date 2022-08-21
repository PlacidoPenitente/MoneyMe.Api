using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IQuoteFactory _quoteFactory;
        private readonly IProductRepository _productRepository;

        public QuoteService(IQuoteRepository quoteRepository, IQuoteFactory quoteFactory, IProductRepository productRepository)
        {
            _quoteRepository = quoteRepository;
            _quoteFactory = quoteFactory;
            _productRepository = productRepository;
        }

        public async Task<string> RequestQuoteAsync(QuoteDto quoteDto)
        {
            var product = await _productRepository.FindByNumberOfTerms(quoteDto.Terms);

            var quote = _quoteFactory.Create(quoteDto.CustomerId, quoteDto.AmountRequired);

            quote.SelectProduct(product);

            await _quoteRepository.AddAsync(quote);

            return await Task.FromResult($"http://localhost:5001/api/quote/continue/{quote.Id}");
        }

        public async Task<QuoteDto> ContinueQuoteAsync(Guid quoteId)
        {
            var quote = await _quoteRepository.GetAsync(quoteId);

            return new QuoteDto
            {
                Id = quote.Id,
                AmountRequired = quote.LoanAmount,
                Terms = quote.Terms,
                CustomerId = quote.CustomerId
            };
        }

        public async Task<QuoteDto> CalculateAsync(Guid quoteId, Guid productId)
        {
            var quote = await _quoteRepository.GetAsync(quoteId);
            var product = await _productRepository.GetAsync(productId);

            quote.SelectProduct(product);

            return new QuoteDto
            {
                Id = quote.Id,
                AmountRequired = quote.LoanAmount,
                Terms = quote.Terms,
                CustomerId = quote.CustomerId
            };
        }
    }
}