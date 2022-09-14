using MoneyMe.Domain.RuleAggregate;
using System;

namespace MoneyMe.Domain.Factories
{
    public class RuleFactory : IRuleFactory
    {
        public Rule Create(string name)
        {
            return new Rule(Guid.NewGuid(), DateTime.UtcNow, null, name.Trim());
        }
    }
}