using MoneyMe.Domain.FeeAggregate;
using System;

namespace MoneyMe.Domain.Factories
{
    public class FeeFactory : IFeeFactory
    {
        public Fee Create(string name, decimal amount)
        {
            return new Fee(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, name, amount);
        }
    }
}