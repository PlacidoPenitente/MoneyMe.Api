using System;

namespace MoneyMe.Api.Responses.Product
{
    public class Fee
    {
        public Fee(Guid id, string name, decimal amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }

        public Guid Id { get; }
        public string Name { get; }
        public decimal Amount { get; }
    }
}