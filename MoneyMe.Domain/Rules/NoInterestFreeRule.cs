using Microsoft.VisualBasic;
using MoneyMe.Domain.LoanAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Rules
{
    public sealed class NoInterestFreeRule : IRule
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

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