using MoneyMe.Domain.RuleAggregate;

namespace MoneyMe.Domain.Factories
{
    public interface IRuleFactory
    {
        Rule Create(string name);
    }
}