using MoneyMe.Domain.ProductAggregate;

namespace MoneyMe.Domain.Factories
{
    public interface IFeeFactory
    {
        Fee Create(string name, decimal amount);
    }
}