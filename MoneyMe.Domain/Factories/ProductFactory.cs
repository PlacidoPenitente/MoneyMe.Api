using MoneyMe.Domain.FeeAggregate;
using MoneyMe.Domain.ProductAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Factories
{
    public class ProductFactory : IProductFactory
    {
        public Product Create(
            string name,
            decimal interestRate,
            int minimumDuration,
            int maximumDuration,
            string rule)
        {
            return new Product(Guid.NewGuid(), DateTime.UtcNow, null, name, interestRate, minimumDuration, maximumDuration, rule);
        }
    }
}