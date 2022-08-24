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
            decimal loanAmount)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            CustomerId = customerId;
            LoanAmount = loanAmount;
        }

        public Guid Id { get; private set; }
        public DateTime DateAdded { get; private set; }
        public DateTime DateModified { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal LoanAmount { get; private set; }
        public int Terms { get; private set; }
        public decimal MonthlyPayment { get; private set; }

        public void ApplyProductTerms(Product product)
        {
            Terms = product.Terms;
            MonthlyPayment = product.CalculateMonthlyPayment(LoanAmount);
        }
    }
}