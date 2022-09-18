using System;
using System.Collections.Generic;
using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.Shared;

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
            return new Loan(Guid.NewGuid(), DateTime.UtcNow, null, customerId, loanAmount, monthlyAmortization);
        }
    }
}