using System.Collections.Generic;
using MoneyMe.Domain.Shared;

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