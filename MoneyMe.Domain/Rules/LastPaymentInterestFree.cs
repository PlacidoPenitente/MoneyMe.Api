using Microsoft.VisualBasic;
using MoneyMe.Domain.LoanAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyMe.Domain.Rules
{
    public sealed class LastPaymentInterestFree : IRule
    {
        public string Name { get; private set; }

        public IReadOnlyCollection<Term> GenerateTerms(decimal loanAmount, int terms, decimal interestRate, decimal monthlyPayment)
        {
            var paymentTerms = new List<Term>();

            for (int period = 1; period < terms + 1; period++)
            {
                var interest = Financial.IPmt(decimal.ToDouble(interestRate), period, terms, decimal.ToDouble(loanAmount));
                paymentTerms.Add(new Term(period, 0, monthlyPayment));
            }

            return paymentTerms;
        }
    }
}