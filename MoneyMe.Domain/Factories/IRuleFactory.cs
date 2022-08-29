using MoneyMe.Domain.Rules;

namespace MoneyMe.Domain.Factories
{
    public interface IRuleFactory
    {
        IRule CreateRule(string ruleName);
    }
}