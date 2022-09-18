using System;
using System.Collections.Generic;

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

        public Guid Id { get; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal LoanAmount { get; private set; }
        public int Term { get; private set; }
        public decimal Interest { get; private set; }
        public decimal MonthlyPayment { get; private set; }

        internal void ChangeLoanAmount(decimal loanAmont)
        {
            LoanAmount = loanAmont;
            DateModified = DateTime.UtcNow;
        }

        internal void ChangeTerm(int term)
        {
            Term = term;
            DateModified = DateTime.UtcNow;
        }

        internal void ChangeInterest(decimal interest)
        {
            Interest = interest;
            DateModified = DateTime.UtcNow;
        }

        internal void ChangeMonthlypayment(decimal monthlyPayment)
        {
            MonthlyPayment = monthlyPayment;
            DateModified = DateTime.UtcNow;
        }
    }
}