using MoneyMe.Application.Contracts;
using MoneyMe.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class LoanService : ILoanService
    {
        private readonly IQuoteRepository _quoteRepository;

        public LoanService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task AppyAsync(Guid quoteId)
        {
            var quote = await _quoteRepository.GetAsync(quoteId);
        }
    }
}