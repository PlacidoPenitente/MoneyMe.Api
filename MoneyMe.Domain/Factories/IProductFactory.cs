using MoneyMe.Domain.FeeAggregate;
using MoneyMe.Domain.ProductAggregate;
using System.Collections.Generic;
using System;

namespace MoneyMe.Domain.Factories
{
    public interface IProductFactory
    {
        Product Create(Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            string name,
            decimal interestRate,
            int maximumDuration,
            int minimumDuration,
            string rule,
            IReadOnlyCollection<Fee> fees);
    }
}