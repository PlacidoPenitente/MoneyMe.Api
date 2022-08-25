using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class LoanService : ILoanService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly ILoanFactory _loanFactory;
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LoanService(IQuoteRepository quoteRepository, ILoanFactory loanFactory, ILoanRepository loanRepository, IUnitOfWork unitOfWork)
        {
            _quoteRepository = quoteRepository;
            _loanFactory = loanFactory;
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoanDto> ApplyAsync(Guid quoteId)
        {
            var quote = await _quoteRepository.GetAsync(quoteId);
            var loan = _loanFactory.Create(quote.CustomerId, quote.LoanAmount, quote.Terms, quote.MonthlyPayment, quote.InterestRate);

            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () => await _loanRepository.AddAsync(loan));
            }

            return new LoanDto
            {
                Id = loan.Id,
                DateAdded = loan.DateAdded,
                DateModified = loan.DateModified,
                Terms = loan.Terms.Select(CreateTermDto).ToList(),
                Status = loan.Status
            };
        }

        private TermDto CreateTermDto(Term term)
        {
            return new TermDto
            {
                Interest = term.Interest,
                Period = term.Period,
                Principal = term.Principal
            };
        }
    }
}