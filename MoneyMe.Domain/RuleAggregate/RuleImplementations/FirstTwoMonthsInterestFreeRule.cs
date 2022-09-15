using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using MoneyMe.Domain.Shared;

namespace MoneyMe.Domain.RuleAggregate.RuleImplementations
{
    public sealed class FirstTwoMonthsInterestFreeRule : IRuleImplementation
    {
        public IReadOnlyCollection<Payment> GenerateMonthlyAmortization(decimal loanAmount, int term, decimal interestRate, decimal monthlyPayment)
        {
            var monthlyAmortization = new List<Payment>();

            for (int period = 1; period < term + 1; period++)
            {
                var interest = Financial.IPmt(decimal.ToDouble(interestRate), period, term, decimal.ToDouble(loanAmount));
                monthlyAmortization.Add(new Payment(period, 0, monthlyPayment - Convert.ToDecimal(interest)));
            }

            return monthlyAmortization;
        }
    }
}