using System;
using MoneyMe.Domain.FeeAggregate;

namespace MoneyMe.Domain.Factories
{
    public class FeeFactory : IFeeFactory
    {
        public Fee Create(string name, decimal amount, bool isPercentage)
        {
            return new Fee(Guid.NewGuid(), DateTime.UtcNow, null, name.Trim(), amount, isPercentage);
        }
    }
}