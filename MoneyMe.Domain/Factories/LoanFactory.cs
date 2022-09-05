using Microsoft.VisualBasic;
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
            int term,
            decimal interestRate,
            IReadOnlyCollection<Payment> monthlyAmortization)
        {
            return new Loan(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, customerId, loanAmount, monthlyAmortization);
        }
    }
}