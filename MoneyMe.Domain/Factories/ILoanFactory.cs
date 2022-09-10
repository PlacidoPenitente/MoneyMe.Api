using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.Shared;
using System;
using System.Collections.Generic;

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