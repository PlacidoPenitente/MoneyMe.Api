using MoneyMe.Domain.FeeAggregate;
using MoneyMe.Domain.ProductAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Factories
{
    public class ProductFactory : IProductFactory
    {
        public Product Create(
            Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            string name,
            decimal interestRate,
            int maximumDuration,
            int minimumDuration,
            string rule,
            IReadOnlyCollection<Fee> fees)
        {
            return new Product(Guid.NewGuid(), DateTime.UtcNow, null, name, interestRate, maximumDuration, minimumDuration, rule, fees);
        }
    }
}