using System;
using Microsoft.VisualBasic;

namespace MoneyMe.Domain.ProductAggregate
{
    public class Product : IAggregate<Guid>
    {
        public Product(
            Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            string name,
            decimal interestPerAnnum,
            int terms)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            Name = name;
            InterestRate = interestPerAnnum;
            Terms = terms;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public string Name { get; }
        public decimal InterestRate { get; }
        public int Terms { get; }

        public decimal Calculate(decimal loanAmount)
        {
            var pmt = Financial.Pmt(decimal.ToDouble(InterestRate) / 12, Terms, decimal.ToDouble(loanAmount));
            return Convert.ToDecimal(pmt);
        }
    }
}