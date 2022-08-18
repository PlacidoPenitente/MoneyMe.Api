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
            decimal interestPerAnnum)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            Name = name;
            InterestPerAnnum = interestPerAnnum;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public string Name { get; }
        public decimal InterestPerAnnum { get; }

        internal decimal Calculate(decimal loanAmount, int term)
        {
            var pmt = Financial.Pmt(decimal.ToDouble(InterestPerAnnum) / 12, term, decimal.ToDouble(loanAmount));
            return Convert.ToDecimal(pmt);
        }
    }
}