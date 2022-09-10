using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Domain.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class LoanService : ILoanService
    {
        private readonly IProductRepository _productRepository;
        private readonly IQuoteRepository _quoteRepository;
        private readonly ILoanFactory _loanFactory;
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LoanService(
            IProductRepository productRepository,
            IQuoteRepository quoteRepository,
            ILoanFactory loanFactory,
            ILoanRepository loanRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _quoteRepository = quoteRepository;
            _loanFactory = loanFactory;
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoanDto> ApplyAsync(Guid quoteId, Guid productId)
        {
            var quote = await _quoteRepository.GetAsync(quoteId);

            var product = await _productRepository.GetAsync(productId);

            var monthlyAmortization = product.CalculateMonthlyAmortization(quote.LoanAmount, quote.Term);

            var loan = _loanFactory.Create(quote.CustomerId, quote.LoanAmount, quote.Term, quote.Interest, monthlyAmortization);

            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () => await _loanRepository.AddAsync(loan));
            }

            return new LoanDto
            {
                Id = loan.Id,
                DateAdded = loan.DateCreated,
                DateModified = loan.DateModified.Value,
                PeriodicPayments = loan.MonthlyAmortization.Select(CreateTermDto).ToList(),
                Status = loan.Status
            };
        }

        private PeriodicPaymentDto CreateTermDto(Payment periodicPayment)
        {
            return new PeriodicPaymentDto
            {
                Interest = periodicPayment.Interest,
                Period = periodicPayment.Period,
                Principal = periodicPayment.Principal
            };
        }
    }
}