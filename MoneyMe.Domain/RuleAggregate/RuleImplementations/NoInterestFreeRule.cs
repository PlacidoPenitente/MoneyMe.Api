using Microsoft.VisualBasic;
using MoneyMe.Domain.RuleAggregate;
using MoneyMe.Domain.Shared;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Rules
{
    public sealed class NoInterestFreeRule : IRuleImplementation
    {
        public IReadOnlyCollection<Payment> GenerateMonthlyAmortization(decimal loanAmount, int term, decimal interestRate, decimal monthlyPayment)
        {
            var payments = new List<Payment>();

            for (int period = 1; period < term + 1; period++)
            {
                var interest = Convert.ToDecimal(Financial.IPmt(decimal.ToDouble(interestRate)/12, period, term, decimal.ToDouble(decimal.Negate(loanAmount))));
                payments.Add(new Payment(period, interest, monthlyPayment - interest));
            }

            return payments;
        }
    }
}