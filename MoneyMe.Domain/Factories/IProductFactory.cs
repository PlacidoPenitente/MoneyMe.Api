using MoneyMe.Domain.ProductAggregate;
using System;

namespace MoneyMe.Domain.Factories
{
    public interface IProductFactory
    {
        Product Create(
            string name,
            decimal interestRate,
            int minimumDuration,
            int maximumDuration,
            Guid ruleId);
    }
}