using MoneyMe.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyMe.Domain.LoanAggregate
{
    public class Loan : IAggregate<Guid>
    {
        private readonly List<Payment> _monthlyAmortization;

        public Loan(
            Guid id,
            DateTime dateAdded,
            DateTime? dateModified,
            Guid customerId,
            decimal loanAmount,
            IReadOnlyCollection<Payment> term)
        {
            Id = id;
            DateCreated = dateAdded;
            DateModified = dateModified;
            CustomerId = customerId;
            LoanAmount = loanAmount;
            _monthlyAmortization = term.ToList();
        }

        public Guid Id { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal LoanAmount { get; private set; }
        public IReadOnlyCollection<Payment> MonthlyAmortization => _monthlyAmortization;
        public LoanStatus Status { get; private set; }

        public void Approve()
        {
            if (Status == LoanStatus.Pending)
            {
                Status = LoanStatus.Active;
            }
        }

        public void Decline()
        {
            if (Status == LoanStatus.Pending)
            {
                Status = LoanStatus.Declined;
            }
        }

        public decimal Pay(decimal amount)
        {
            if (Status != LoanStatus.Active)
            {
                return amount;
            }

            while (amount > 0)
            {
                var unpaidPeriodicPayment = _monthlyAmortization.OrderBy(term => term.Period).FirstOrDefault(term => term.Principal > 0);

                if (unpaidPeriodicPayment == null)
                {
                    Status = LoanStatus.Completed;
                    break;
                }

                var remaining = unpaidPeriodicPayment.Principal - amount;
                amount = 0;

                if (remaining < 0)
                {
                    remaining = 0;
                    amount = decimal.Negate(remaining);
                }

                var paidPeriodicPayment = new Payment(unpaidPeriodicPayment.Period, unpaidPeriodicPayment.Interest, remaining);

                _monthlyAmortization.Add(paidPeriodicPayment);
                _monthlyAmortization.Remove(unpaidPeriodicPayment);

                DateModified = DateTime.UtcNow;
            }

            return amount;
        }
    }
}