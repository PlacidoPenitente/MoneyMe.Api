using Microsoft.VisualBasic;
using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.RuleAggregate;
using MoneyMe.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyMe.Domain.RuleAggregate.RuleImplementations
{
    public sealed class LastPaymentInterestFree : IRuleImplementation
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