using MoneyMe.Domain.Shared;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Rules
{
    public interface IRule
    {
        string Name { get; }

        IReadOnlyCollection<Payment> GenerateMonthlyAmortization(decimal loanAmount, int term, decimal interestRate, decimal monthlyPayment);
    }
}