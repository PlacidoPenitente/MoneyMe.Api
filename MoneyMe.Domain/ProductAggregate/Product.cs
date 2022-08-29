using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.LoanAggregate;

namespace MoneyMe.Domain.ProductAggregate
{
    public class Product : IAggregate<Guid>
    {
        private readonly RuleFactory _ruleFactory;

        private Product()
        {
            _ruleFactory = new RuleFactory();
        }

        public Product(
            Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            string name,
            decimal interestRate,
            int maximumDuration,
            int minimumDuration,
            string rule)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            Name = name;
            InterestRate = interestRate;
            MaximumDuration = maximumDuration;
            MinimumDuration = minimumDuration;
            Rule = rule;

            _ruleFactory = new RuleFactory();
        }

        [Key]
        public Guid Id { get; private set; }

        public DateTime DateAdded { get; private set; }

        public DateTime DateModified { get; private set; }

        public string Name { get; private set; }

        [Column(TypeName = "decimal(5, 4)")]
        public decimal InterestRate { get; private set; }

        public int MaximumDuration { get; private set; }

        public int MinimumDuration { get; private set; }

        public string Rule { get; private set; }

        public IReadOnlyCollection<Term> CalculateMonthlyAmortization(decimal loanAmount, int term)
        {
            var rule = _ruleFactory.CreateRule(Rule);

            if (term > MaximumDuration || term <MinimumDuration)
            {
                throw new Exception($"This product does not support the desired term.");
            }

            var payment = Financial.Pmt(decimal.ToDouble(InterestRate) / 12, term, decimal.ToDouble(decimal.Negate(loanAmount)));

            return rule.GenerateTerms(loanAmount, term, InterestRate, Convert.ToDecimal(payment));
        }
    }
}