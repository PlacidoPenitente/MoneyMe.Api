using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IQuoteFactory _quoteFactory;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public QuoteService(
            IQuoteRepository quoteRepository,
            IQuoteFactory quoteFactory,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _quoteRepository = quoteRepository;
            _quoteFactory = quoteFactory;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<QuoteDto> CalculateAsync(PartialQuoteDto partialQuoteDto)
        {
            var product = await _productRepository.GetAsync(partialQuoteDto.ProductId);

            var quote = _quoteFactory.Create(partialQuoteDto.CustomerId, partialQuoteDto.AmountRequired, partialQuoteDto.Term);

            quote.ApplyProductTerms(product);

            await using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () =>
                {
                    await _quoteRepository.AddAsync(quote);
                });
            };

            return new QuoteDto
            {
                Id = quote.Id,
                DateAdded = quote.DateAdded,
                DateModified = quote.DateModified,
                CustomerId = quote.CustomerId,
                LoanAmount = quote.LoanAmount,
                Terms = quote.Term,
                MonthlyPayment = 100
            };
        }

        public async Task<QuoteDto> GetQuoteAsync(Guid id)
        {
            var quote = await _quoteRepository.GetAsync(id);

            return new QuoteDto
            {
                Id = id,
                DateAdded = quote.DateAdded,
                DateModified = quote.DateModified,
                CustomerId = quote.CustomerId,
                LoanAmount = quote.LoanAmount,
                MonthlyPayment = quote.MonthlyAmotization.Average(x => x.Principal + x.Interest),
                Terms = quote.Term
            };
        }
    }
}