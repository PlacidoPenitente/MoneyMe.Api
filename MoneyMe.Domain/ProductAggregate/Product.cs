using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyMe.Domain.ProductAggregate
{
    public class Product : IAggregate<Guid>
    {
        private List<Guid> _feeIds;

        public Product(
            Guid id,
            DateTime dateAdded,
            DateTime? dateModified,
            string name,
            decimal interestRate,
            int minimumDuration,
            int maximumDuration,
            Guid ruleId,
            List<Guid> feeIds)
        {
            Id = id;
            DateCreated = dateAdded;
            DateModified = dateModified;
            Name = name;
            InterestRate = interestRate;
            MinimumDuration = minimumDuration;
            MaximumDuration = maximumDuration;
            RuleId = ruleId;
            _feeIds = feeIds;
        }

        public Guid Id { get; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public string Name { get; private set; }
        public decimal InterestRate { get; private set; }
        public int MaximumDuration { get; private set; }
        public int MinimumDuration { get; private set; }
        public Guid RuleId { get; private set; }
        public IReadOnlyCollection<Guid> FeeIds => _feeIds;

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

        public void ChangeFees(List<Guid> feeIds)
        {
            if (feeIds != null)
            {
                var toRemove = new List<Guid>();

                foreach (var feeId in _feeIds)
                {
                    if (!feeIds.Any(x => x == feeId))
                    {
                        toRemove.Add(feeId);
                    }
                }

                foreach (var feeId in feeIds)
                {
                    if (!_feeIds.Any(x => x == feeId))
                    {
                        _feeIds.Add(feeId);
                    }
                }

                _feeIds.RemoveAll(x => toRemove.Any(c => c == x));

                DateModified = DateTime.UtcNow;
            }
        }

        public void ChangeRule(Guid? ruleId)
        {
            if (ruleId.HasValue && ruleId.Value != Guid.Empty)
            {
                RuleId = ruleId.Value;
                DateModified = DateTime.UtcNow;
            }
        }

        public void AddFee(Guid? feeId)
        {
            if (feeId.HasValue && feeId.Value != Guid.Empty && !_feeIds.Any(x => x == feeId.Value))
            {
                _feeIds.Add(feeId.Value);
                DateModified = DateTime.UtcNow;
            }
        }

        public void RemoveFee(Guid? feeId)
        {
            if (feeId.HasValue && feeId.Value != Guid.Empty && _feeIds.Any(x => x == feeId.Value))
            {
                _feeIds.Remove(feeId.Value);
                DateModified = DateTime.UtcNow;
            }
        }
    }
}