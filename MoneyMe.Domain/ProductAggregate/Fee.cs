namespace MoneyMe.Domain.ProductAggregate
{
    public class Fee
    {
        public Fee(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }

        public string Name { get; private set; }
        public decimal Amount { get; private set; }
    }
}