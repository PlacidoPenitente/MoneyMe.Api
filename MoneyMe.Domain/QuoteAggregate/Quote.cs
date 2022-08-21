using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.Shared;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.QuoteAggregate
{
    public class Quote : IAggregate<Guid>
    {
        private readonly HashSet<Fee> _fees;

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

        public void CalculateMonthlyPayment(Product product)
        {
            Terms = product.Terms;
            MonthlyPayment = product.CalculateMonthlyPayment(LoanAmount);
        }
    }
}