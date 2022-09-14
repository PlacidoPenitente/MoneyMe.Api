using System;

namespace MoneyMe.Domain.ProductAggregate
{
    public class Product : IAggregate<Guid>
    {
        public Product(
            Guid id,
            DateTime dateAdded,
            DateTime? dateModified,
            string name,
            decimal interestRate,
            int minimumDuration,
            int maximumDuration,
            Guid ruleId)
        {
            Id = id;
            DateCreated = dateAdded;
            DateModified = dateModified;
            Name = name;
            InterestRate = interestRate;
            MinimumDuration = minimumDuration;
            MaximumDuration = maximumDuration;
            RuleId = ruleId;
        }

        public Guid Id { get; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public string Name { get; private set; }
        public decimal InterestRate { get; private set; }
        public int MaximumDuration { get; private set; }
        public int MinimumDuration { get; private set; }
        public Guid RuleId { get; private set; }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid product name.");
            }

            Name = name.Trim();
            DateModified = DateTime.UtcNow;
        }

        public void ChangeInterestRate(decimal interestRate)
        {
            InterestRate = interestRate;
            DateModified = DateTime.UtcNow;
        }

        public void ChangeDurationRange(int minimumDuration, int maximumDuration)
        {
            if (maximumDuration < minimumDuration || minimumDuration < 0)
            {
                throw new ArgumentException("Invalid duration range.");
            }

            MinimumDuration = minimumDuration;
            MaximumDuration = maximumDuration;
            DateModified = DateTime.UtcNow;
        }

        public void ChangeRule(Guid ruleId)
        {
            RuleId = ruleId;
            DateModified = DateTime.UtcNow;
        }
    }
}