using System;

namespace MoneyMe.Domain.FeeAggregate
{
    public class Fee : IAggregate<Guid>
    {
        public Fee(
            Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            string name,
            decimal amount)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            Name = name;
            Amount = amount;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public string Name { get; }
        public decimal Amount { get; }
    }
}