using System;
using System.Collections.Generic;
using MoneyMe.Domain.ProductAggregate;

namespace MoneyMe.Domain.Factories
{
    public interface IProductFactory
    {
        Product Create(
            string name,
            decimal interestRate,
            int minimumDuration,
            int maximumDuration,
            Guid ruleId,
            List<Guid> feeIds);

        Product Create(
            Guid id,
            DateTime dateCreated,
            DateTime? dateModified,
            string name,
            decimal interestRate,
            int minimumDuration,
            int maximumDuration,
            Guid ruleId,
            List<Guid> feeIds);
    }
}