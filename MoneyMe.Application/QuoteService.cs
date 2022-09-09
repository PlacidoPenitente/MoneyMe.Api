using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
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

            var quote = _quoteFactory.Create(partialQuoteDto.CustomerId, partialQuoteDto.LoanAmount, partialQuoteDto.Term);

            quote.ApplyProductTerm(product);

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
                DateAdded = quote.DateCreated,
                DateModified = quote.DateModified.Value,
                CustomerId = quote.CustomerId,
                ProductId = quote.ProductId,
                LoanAmount = quote.LoanAmount,
                Term = quote.Term,
                Interest = quote.Interest,
                Fee = quote.Fee,
                MonthlyPayment = quote.MonthlyPayment
            };
        }

        public async Task<QuoteDto> GetQuoteAsync(Guid id)
        {
            var quote = await _quoteRepository.GetAsync(id);

            return new QuoteDto
            {
                Id = quote.Id,
                DateAdded = quote.DateCreated,
                DateModified = quote.DateModified.Value,
                CustomerId = quote.CustomerId,
                ProductId = quote.ProductId,
                LoanAmount = quote.LoanAmount,
                Term = quote.Term,
                Interest = quote.Interest,
                Fee = quote.Fee,
                MonthlyPayment = quote.MonthlyPayment
            };
        }
    }
}