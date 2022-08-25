using Microsoft.VisualBasic;
using MoneyMe.Domain.ApplicationAggregate;
using MoneyMe.Domain.LoanAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Factories
{
    public class LoanFactory : ILoanFactory
    {
        public Loan Create(
            Guid customerId,
            decimal loanAmount,
            int terms,
            decimal monthlyPayment,
            decimal interestRate)
        {
            var paymentTerms = new List<Term>();

            for (int period = 1; period < terms + 1; period++)
            {
                var interest = Financial.IPmt(decimal.ToDouble(interestRate), period, terms, decimal.ToDouble(loanAmount));
                paymentTerms.Add(new Term(period, Convert.ToDecimal(interest), monthlyPayment));
            }

            return new Loan(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, customerId, loanAmount, paymentTerms);
        }
    }
}