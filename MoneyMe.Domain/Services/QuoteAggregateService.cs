using Microsoft.VisualBasic;
using MoneyMe.Domain.QuoteAggregate;
using MoneyMe.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Services
{
    public class QuoteAggregateService : IQuoteAggregateService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRuleRepository _ruleRepository;
        private readonly IFeeRepository _feeRepository;
        private readonly IQuoteRepository _quoteRepository;

        public QuoteAggregateService(IQuoteRepository quoteRepository, IProductRepository productRepository, IRuleRepository ruleRepository, IFeeRepository feeRepository)
        {
            _quoteRepository = quoteRepository;
            _productRepository = productRepository;
            _ruleRepository = ruleRepository;
            _feeRepository = feeRepository;
        }

        public async Task ChangeTermAsync(Quote quote, int term)
        {
            quote.ChangeTerm(term);
            await CalculateQuoteAsync(quote);
        }

        public async Task ChangeLoanAmountAsync(Quote quote, decimal loanAmount)
        {
            quote.ChangeLoanAmount(loanAmount);
            await CalculateQuoteAsync(quote);
        }

        private async Task CalculateQuoteAsync(Quote quote)
        {
            var product = await _productRepository.GetAsync(quote.ProductId);
            var rule = await _ruleRepository.GetAsync(product.RuleId);
            var totalFee = 0m;

            foreach (var feeId in quote.FeeIds)
            {
                var fee = await _feeRepository.GetAsync(feeId);

                if (fee.IsPercentage)
                {
                    var amount = (fee.Amount / 100) * quote.LoanAmount;

                    totalFee += amount;
                }
                else
                {
                    totalFee += fee.Amount;
                }
            }

            var loanAmount = quote.LoanAmount + totalFee;
            var payment = Financial.Pmt(decimal.ToDouble(product.InterestRate) / 12, quote.Term, decimal.ToDouble(decimal.Negate(loanAmount)));

            var payments = rule.GenerateMonthlyAmortization(loanAmount, quote.Term, product.InterestRate, Convert.ToDecimal(Math.Round(payment, 2)));

            quote.ChangeInterest(payments.Sum(payment => payment.Interest));
            quote.ChangeMonthlypayment(payments.Average(payment => payment.Interest + payment.Principal));

            _quoteRepository.Update(quote);
        }
    }
}