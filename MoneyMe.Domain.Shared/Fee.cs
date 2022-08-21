namespace MoneyMe.Domain.Shared
{
    public class Fee
    {
        public Fee(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }

        public string Name { get; }
        public decimal Amount { get; }
    }
}