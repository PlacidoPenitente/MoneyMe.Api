using System;

namespace MoneyMe.Domain.FeeAggregate
{
    public class Fee : IAggregate<Guid>
    {
        public Fee(
            Guid id,
            DateTime dateCreated,
            DateTime? dateModified,
            string name,
            decimal amount,
            bool isPercentage)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
            Name = name;
            Amount = amount;
            IsPercentage = isPercentage;
        }

        public Guid Id { get; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public string Name { get; private set; }
        public decimal Amount { get; private set; }
        public bool IsPercentage { get; private set; }

        public void SetAsPercentage()
        {
            IsPercentage = true;
            DateModified = DateTime.UtcNow;
        }

        public void SetAsFixedAmount()
        {
            IsPercentage = false;
            DateModified = DateTime.UtcNow;
        }

        public void ChangeName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name.Trim();
                DateModified = DateTime.UtcNow;
            }
        }

        public void ChangeAmount(decimal? amount)
        {
            if (amount.HasValue && amount > -1)
            {
                Amount = amount.Value;
                DateModified = DateTime.UtcNow;
            }
        }
    }
}