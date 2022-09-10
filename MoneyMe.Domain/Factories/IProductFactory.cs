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
            string rule);
    }
}