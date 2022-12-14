using MoneyMe.Domain.FeeAggregate;

namespace MoneyMe.Domain.Factories
{
    public interface IFeeFactory
    {
        Fee Create(string name, decimal amount, bool isPercentage);
    }
}