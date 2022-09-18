using System;
using System.Collections.Generic;
using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.Shared;

namespace MoneyMe.Domain.Factories
{
    public interface ILoanFactory
    {
        Loan Create(
            Guid customerId,
            decimal loanAmount,
            int term,
            decimal interestRate,
            IReadOnlyCollection<Payment> monthlyAmortization);
    }
}