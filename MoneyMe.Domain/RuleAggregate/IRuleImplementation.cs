using MoneyMe.Domain.Shared;
using System.Collections.Generic;

namespace MoneyMe.Domain.RuleAggregate
{
    public interface IRuleImplementation
    {
        IReadOnlyCollection<Payment> GenerateMonthlyAmortization(
            decimal loanAmount,
            int term,
            decimal interestRate,
            decimal monthlyPayment);
    }
}