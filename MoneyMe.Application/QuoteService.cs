using AutoMapper;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.Repositories;
using MoneyMe.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IQuoteFactory _quoteFactory;
        private readonly IProductRepository _productRepository;
        private readonly IQuoteAggregateService _quoteAggregateService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuoteService(
            IQuoteRepository quoteRepository,
            IQuoteFactory quoteFactory,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IQuoteAggregateService quoteAggregateService,
            IMapper mapper)
        {
            _quoteRepository = quoteRepository;
            _quoteFactory = quoteFactory;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _quoteAggregateService = quoteAggregateService;
            _mapper = mapper;
        }

        public async Task<QuoteDto> CalculateAsync(PartialQuoteDto partialQuoteDto)
        {
            var product = await _productRepository.GetAsync(partialQuoteDto.ProductId);

            var quote = _quoteFactory.Create(partialQuoteDto.CustomerId, partialQuoteDto.LoanAmount, partialQuoteDto.Term, new List<Guid>());



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
                MonthlyPayment = quote.MonthlyPayment
            };
        }

        public async Task<QuoteDto> ReadQuoteAsync(Guid id)
        {
            var quote = await _quoteRepository.GetAsync(id);

            return _mapper.Map<QuoteDto>(quote);
        }

        public async Task<IEnumerable<QuoteDto>> ReadAllQuotesAsync()
        {
            var quotes = await _quoteRepository.GetAllAsync();

            return quotes.Select(_mapper.Map<QuoteDto>);
        }

        public async Task<QuoteDto> UpdateQuoteAsync(QuoteDto quoteDto)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    var quote = await _quoteRepository.GetAsync(quoteDto.Id);

                    _quoteRepository.Update(quote);

                    return _mapper.Map<QuoteDto>(quote);
                });
            }
        }

        public async Task DeleteQuoteAsync(Guid id)
        {
            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () =>
                {
                    var quote = await _quoteRepository.GetAsync(id);

                    _quoteRepository.Remove(quote);
                });
            }
        }
    }
}