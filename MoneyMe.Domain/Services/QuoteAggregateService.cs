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

        public async Task<decimal> ChangeTermAsync(Quote quote, int term)
        {
            quote.ChangeTerm(term);
            return await CalculateQuoteAsync(quote);
        }

        public async Task<decimal> ChangeLoanAmountAsync(Quote quote, decimal loanAmount)
        {
            quote.ChangeLoanAmount(loanAmount);
            return await CalculateQuoteAsync(quote);
        }

        public async Task<decimal> CalculateQuoteAsync(Quote quote)
        {
            var product = await _productRepository.GetAsync(quote.ProductId);
            var rule = await _ruleRepository.GetAsync(product.RuleId);
            var totalFee = 0m;

            foreach (var feeId in product.FeeIds)
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
            var interestRate = product.InterestRate / 100;

            var payment = Financial.Pmt(decimal.ToDouble(interestRate) / 12, quote.Term, decimal.ToDouble(decimal.Negate(loanAmount)));
            var payments = rule.GenerateMonthlyAmortization(loanAmount, quote.Term, interestRate, Convert.ToDecimal(Math.Round(payment, 2)));

            quote.ChangeInterest(payments.Sum(payment => payment.Interest));
            quote.ChangeMonthlypayment(payments.Average(payment => payment.Interest + payment.Principal));

            return totalFee;
        }
    }
}