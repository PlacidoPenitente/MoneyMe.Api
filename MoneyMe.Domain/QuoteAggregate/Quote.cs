using MoneyMe.Domain.ProductAggregate;
using System;
using System.Linq;

namespace MoneyMe.Domain.QuoteAggregate
{
    public class Quote : IAggregate<Guid>
    {
        public Quote(
            Guid id,
            DateTime dateAdded,
            DateTime? dateModified,
            Guid customerId,
            decimal loanAmount,
            int term)
        {
            Id = id;
            DateCreated = dateAdded;
            DateModified = dateModified;
            CustomerId = customerId;
            LoanAmount = loanAmount;
            Term = term;
        }

        public Guid Id { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal LoanAmount { get; private set; }
        public int Term { get; private set; }
        public decimal Interest { get; private set; }
        public decimal Fee { get; private set; }
        public decimal MonthlyPayment { get; private set; }

        public void ChangeTerm(int term)
        {
            Term = term;

            DateModified = DateTime.UtcNow;
        }

        public void ApplyProductTerm(Product product)
        {
            ProductId = product.Id;

            Fee = product.GetTotalFee();

            var payments = product.CalculateMonthlyAmortization(LoanAmount, Term);

            Interest = payments.Sum(payment => payment.Interest);

            MonthlyPayment = payments.Average(payment => payment.Interest + payment.Principal);

            DateModified = DateTime.UtcNow;
        }
    }
}