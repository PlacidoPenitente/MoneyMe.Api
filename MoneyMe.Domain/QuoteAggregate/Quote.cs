using MoneyMe.Domain.ProductAggregate;
using System;

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
            int terms)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            CustomerId = customerId;
            LoanAmount = loanAmount;
            Terms = terms;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public Guid CustomerId { get; }
        public decimal LoanAmount { get; }
        public int Terms { get; }

        public decimal MonthlyPayment { get; private set; }

        public void SelectProduct(Product product)
        {
            MonthlyPayment = product.Calculate(LoanAmount, Terms);
        }
    }
}