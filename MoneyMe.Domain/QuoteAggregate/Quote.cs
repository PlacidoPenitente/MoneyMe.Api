using MoneyMe.Domain.ProductAggregate;
using System;
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
            decimal loanAmount)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            CustomerId = customerId;
            LoanAmount = loanAmount;
        }

        [Key]
        public Guid Id { get; private set; }
        public DateTime DateAdded { get; private set; }
        public DateTime DateModified { get; private set; }
        public Guid CustomerId { get; private set; }

        [Column(TypeName = "decimal(5, 4)")]
        public decimal LoanAmount { get; private set; }

        public int Terms { get; private set; }

        [Column(TypeName = "decimal(5, 4)")]
        public decimal MonthlyPayment { get; private set; }

        [Column(TypeName = "decimal(5, 4)")]
        public decimal InterestRate { get; private set; }

        public void ApplyProductTerms(Product product)
        {
            Terms = product.Terms;
            InterestRate = product.InterestRate;
            MonthlyPayment = product.CalculateMonthlyPayment(LoanAmount);
        }
    }
}