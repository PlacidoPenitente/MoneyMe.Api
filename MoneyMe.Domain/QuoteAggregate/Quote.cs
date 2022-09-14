using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.QuoteAggregate
{
    public class Quote : IAggregate<Guid>
    {
        private readonly List<Guid> _feedIds;

        public Quote(
            Guid id,
            DateTime dateAdded,
            DateTime? dateModified,
            Guid customerId,
            decimal loanAmount,
            int term,
            List<Guid> feeIds)
        {
            Id = id;
            DateCreated = dateAdded;
            DateModified = dateModified;
            CustomerId = customerId;
            LoanAmount = loanAmount;
            Term = term;
            _feedIds = feeIds;
        }

        public Guid Id { get; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal LoanAmount { get; private set; }
        public int Term { get; private set; }
        public decimal Interest { get; private set; }
        public IReadOnlyCollection<Guid> FeeIds => _feedIds;
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

        internal void AddFee(Guid feeId)
        {
            _feedIds.Add(feeId);
            DateModified = DateTime.UtcNow;
        }

        internal void RemoveFee(Guid fee)
        {
            _feedIds.Remove(fee);
            DateModified = DateTime.UtcNow;
        }
    }
}