using System.Collections.Generic;
using Microsoft.VisualBasic;
using MoneyMe.Domain.Shared;

namespace MoneyMe.Domain.RuleAggregate.RuleImplementations
{
    public sealed class LastPaymentInterestFreeRule : IRuleImplementation
    {
        public IReadOnlyCollection<Payment> GenerateMonthlyAmortization(decimal loanAmount, int term, decimal interestRate, decimal monthlyPayment)
        {
            var periodicPayments = new List<Payment>();

            for (int period = 1; period < term + 1; period++)
            {
                var interest = Financial.IPmt(decimal.ToDouble(interestRate), period, term, decimal.ToDouble(loanAmount));
                periodicPayments.Add(new Payment(period, 0, monthlyPayment));
            }

            return periodicPayments;
        }
    }
}