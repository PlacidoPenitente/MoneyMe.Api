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
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name.Trim();
                DateModified = DateTime.UtcNow;
            }
        }

        public void ChangeInterestRate(decimal? interestRate)
        {
            if (interestRate.HasValue)
            {
                InterestRate = interestRate.Value;
                DateModified = DateTime.UtcNow;
            }
        }

        public void ChangeDurationRange(int? minimumDuration, int? maximumDuration)
        {
            if (!minimumDuration.HasValue)
            {
                minimumDuration = MinimumDuration;
            }

            if (!maximumDuration.HasValue)
            {
                maximumDuration = MaximumDuration;
            }

            if (maximumDuration.Value < minimumDuration.Value || minimumDuration.Value < 0)
            {
                throw new ArgumentException("Invalid duration range.");
            }

            MinimumDuration = minimumDuration.Value;
            MaximumDuration = maximumDuration.Value;
            DateModified = DateTime.UtcNow;
        }

        public void ChangeRule(Guid? ruleId)
        {
            if (ruleId.HasValue && ruleId != Guid.Empty)
            {
                RuleId = ruleId.Value;
                DateModified = DateTime.UtcNow;
            }
        }
    }
}