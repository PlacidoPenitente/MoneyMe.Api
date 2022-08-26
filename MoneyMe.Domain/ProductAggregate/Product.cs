using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace MoneyMe.Domain.ProductAggregate
{
    public class Product : IAggregate<Guid>
    {
        private Product()
        {

        }

        public Product(
            Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            string name,
            decimal interestRate,
            int terms)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            Name = name;
            InterestRate = interestRate;
            Terms = terms;
        }

        [Key]
        public Guid Id { get; private set; }
        public DateTime DateAdded { get; private set; }
        public DateTime DateModified { get; private set; }
        public string Name { get; private set; }

        [Column(TypeName = "decimal(5, 4)")]
        public decimal InterestRate { get; private set; }

        public int Terms { get; private set; }

        public decimal CalculateMonthlyPayment(decimal loanAmount)
        {
            var pmt = Financial.Pmt(decimal.ToDouble(InterestRate) / 12, Terms, decimal.ToDouble(decimal.Negate(loanAmount)));

            return Convert.ToDecimal(pmt);
        }
    }
}