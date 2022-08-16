using System;

namespace MoneyMe.Domain.QuoteAggregate
{
    public class Quote : IAggregate<Guid>
    {
        public Quote(
            Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            int term,
            decimal amountRequired,
            Guid customerId)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            Term = term;
            AmountRequired = amountRequired;
            CustomerId = customerId;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public int Term { get; }
        public decimal AmountRequired { get; }
        public Guid CustomerId { get; }
    }
}