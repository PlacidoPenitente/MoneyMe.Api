using System;

namespace MoneyMe.Domain.FeeAggregate
{
    public class Fee : IAggregate<Guid>
    {
        public Fee(Guid id, DateTime dateAdded, DateTime dateModified, string name, decimal amount)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            Name = name;
            Amount = amount;
        }

        public Guid Id { get; private set; }
        public DateTime DateAdded { get; private set; }
        public DateTime DateModified { get; private set; }
        public string Name { get; private set; }
        public decimal Amount { get; private set; }

        public void ChangeName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
                DateModified = DateTime.UtcNow;
            }
        }

        public void ChangeAmount(decimal? amount)
        {
            if (amount.HasValue)
            {
                Amount = amount.Value;
                DateModified = DateTime.UtcNow;
            }
        }
    }
}