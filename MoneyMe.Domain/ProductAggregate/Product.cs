using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using MoneyMe.Domain.RuleAggregate;
using MoneyMe.Domain.Shared;

namespace MoneyMe.Domain.ProductAggregate
{
    public class Product : IAggregate<Guid>
    {
        private readonly RuleSelector _ruleFactory;

        public Product(
            Guid id,
            DateTime dateAdded,
            DateTime? dateModified,
            string name,
            decimal interestRate,
            int minimumDuration,
            int maximumDuration,
            string rule)
        {
            Id = id;
            DateCreated = dateAdded;
            DateModified = dateModified;
            Name = name;
            InterestRate = interestRate;
            MinimumDuration = minimumDuration;
            MaximumDuration = maximumDuration;
            Rule = rule;
            _ruleFactory = new RuleSelector();
        }

        public Guid Id { get; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public string Name { get; private set; }
        public decimal InterestRate { get; private set; }
        public int MaximumDuration { get; private set; }
        public int MinimumDuration { get; private set; }
        public string Rule { get; private set; }

        public IReadOnlyCollection<Payment> CalculateMonthlyAmortization(decimal loanAmount, int term)
        {
            var rule = _ruleFactory.Select(Rule);

            if (term > MaximumDuration || term < MinimumDuration)
            {
                throw new ArgumentException($"This product does not support the desired term.");
            }

            var payment = Financial.Pmt(decimal.ToDouble(InterestRate) / 12, term, decimal.ToDouble(decimal.Negate(loanAmount)));

            return rule.GenerateMonthlyAmortization(loanAmount, term, InterestRate, Convert.ToDecimal(Math.Round(payment, 2)));
        }

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

        public void ChangeRule(string rule)
        {
            if (string.IsNullOrWhiteSpace(rule))
            {
                throw new ArgumentException("Invalid rule.");
            }

            Rule = rule.Trim();
            DateModified = DateTime.UtcNow;
        }
    }
}