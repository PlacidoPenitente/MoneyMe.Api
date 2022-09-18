using MoneyMe.Domain.QuoteAggregate;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Services
{
    public interface IQuoteAggregateService
    {
        Task<decimal> CalculateQuoteAsync(Quote quote);
        Task<decimal> ChangeTermAsync(Quote quote, int term);
        Task<decimal> ChangeLoanAmountAsync(Quote quote, decimal loanAmount);
    }
}