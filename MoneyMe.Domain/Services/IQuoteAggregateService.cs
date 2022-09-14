using MoneyMe.Domain.QuoteAggregate;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Services
{
    public interface IQuoteAggregateService
    {
        Task ChangeTermAsync(Quote quote, int term);
        Task ChangeLoanAmountAsync(Quote quote, decimal loanAmount);
    }
}