using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Domain.QuoteAggregate
{
    public class Quote : IAggregate<Guid>
    {
        public Quote(
            Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            Guid customerId,
            decimal loanAmount,
            int term)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            CustomerId = customerId;
            LoanAmount = loanAmount;
            Term = term;
        }

        [Key]
        public Guid Id { get; private set; }

        public DateTime DateAdded { get; private set; }

        public DateTime DateModified { get; private set; }

        public Guid CustomerId { get; private set; }

        [Column(TypeName = "decimal(5, 4)")]
        public decimal LoanAmount { get; private set; }

        public int Term { get; private set; }

        [Column(TypeName = "decimal(5, 4)")]
        public decimal InterestRate { get; private set; }

        public IReadOnlyCollection<Term> MonthlyAmotization { get; set; }

        public void ApplyProductTerms(Product product)
        {
            InterestRate = product.InterestRate;
            MonthlyAmotization = product.CalculateMonthlyAmortization(LoanAmount, Term);
        }
    }
}