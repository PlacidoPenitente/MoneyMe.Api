using MoneyMe.Domain.LoanAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Rules
{
    public interface IRule
    {
        string Name { get; }

        IReadOnlyCollection<Term> GenerateTerms(decimal loanAmount, int terms, decimal interestRate, decimal monthlyPayment);
    }
}