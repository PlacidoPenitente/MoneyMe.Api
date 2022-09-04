using Microsoft.VisualBasic;
using MoneyMe.Domain.LoanAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Rules
{
    public sealed class InterestFreeRule : IRule
    {
        public string Name { get; private set; }

        public IReadOnlyCollection<Payment> GenerateMonthlyAmortization(decimal loanAmount, int term, decimal interestRate, decimal monthlyPayment)
        {
            var periodicPayments = new List<Payment>();

            for (int period = 1; period < term + 1; period++)
            {
                var interest = Financial.IPmt(decimal.ToDouble(interestRate), period, term, decimal.ToDouble(loanAmount));
                periodicPayments.Add(new Payment(period, 0, monthlyPayment - Convert.ToDecimal(interest)));
            }

            return periodicPayments;
        }
    }
}