using System;
using System.Collections.Generic;
using MoneyMe.Domain.Shared;

namespace MoneyMe.Domain.RuleAggregate
{
    public class Rule : IAggregate<Guid>, IRuleImplementation
    {
        private IRuleImplementation _ruleImplementation;

        public Rule(Guid id, DateTime dateCreated, DateTime? dateModified, string name)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
            Name = SelectRule(name);
        }

        public Guid Id { get; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public string Name { get; private set; }

        public void ChangeName(string name)
        {
            Name = SelectRule(name);
            DateModified = DateTime.Now;
        }

        public IReadOnlyCollection<Payment> GenerateMonthlyAmortization(
            decimal loanAmount,
            int term,
            decimal interestRate,
            decimal monthlyPayment)
        {
            return _ruleImplementation.GenerateMonthlyAmortization(loanAmount, term, interestRate, monthlyPayment);
        }

        private string SelectRule(string ruleName)
        {
            _ruleImplementation = RuleSelector.Instance.Select(ruleName);

            return _ruleImplementation == null ? throw new ArgumentException($"Unable to find rule named {ruleName}.") : ruleName;
        }
    }
}