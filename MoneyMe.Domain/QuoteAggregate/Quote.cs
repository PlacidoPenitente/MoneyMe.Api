using MoneyMe.Domain.FeeAggregate;
using MoneyMe.Domain.ProductAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.QuoteAggregate
{
    public class Quote : IAggregate<Guid>
    {
        private HashSet<Guid> _feeIds;

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
            _feeIds = new HashSet<Guid>();
        }

        public Guid Id { get; private set; }
        public DateTime DateAdded { get; private set; }
        public DateTime DateModified { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal LoanAmount { get; private set; }
        public int Terms { get; private set; }
        public decimal MonthlyPayment { get; private set; }
        public decimal TotalFee { get; private set; }
        public IReadOnlyCollection<Guid> FeeIds => _feeIds;

        public void SelectProduct(Product product)
        {
            Terms = product.Terms;
            MonthlyPayment = product.Calculate(LoanAmount + TotalFee);
        }

        public void ApplyFee(Fee fee)
        {
            _feeIds.Add(fee.Id);
            TotalFee += fee.Amount;
        }

        public void RemoveFee(Fee fee)
        {
            _feeIds.Remove(fee.Id);
            TotalFee -= fee.Amount;
        }
    }
}