using MoneyMe.Domain.ApplicationAggregate;
using System;

namespace MoneyMe.Domain.Factories
{
    public interface ILoanFactory
    {
        Loan Create(
            Guid customerId,
            decimal loanAmount,
            int terms,
            decimal monthlyPayment,
            decimal interestRate);
    }
}