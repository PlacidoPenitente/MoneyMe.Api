using System;
using System.Collections.Generic;
using MoneyMe.Domain.ProductAggregate;

namespace MoneyMe.Domain.Factories
{
    public class ProductFactory : IProductFactory
    {
        public Product Create(
            string name,
            decimal interestRate,
            int minimumDuration,
            int maximumDuration,
            Guid ruleId,
            List<Guid> feeIds)
        {
            return new Product(Guid.NewGuid(), DateTime.UtcNow, null, name, interestRate, minimumDuration, maximumDuration, ruleId, feeIds);
        }

        public Product Create(
            Guid id,
            DateTime dateCreated,
            DateTime? dateModified,
            string name,
            decimal interestRate,
            int minimumDuration,
            int maximumDuration,
            Guid ruleId,
            List<Guid> feeIds)
        {
            return new Product(id, dateCreated, dateModified, name, interestRate, minimumDuration, maximumDuration, ruleId, feeIds);
        }
    }
}