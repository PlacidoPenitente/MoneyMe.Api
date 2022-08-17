using System;

namespace MoneyMe.Domain.QuoteAggregate
{
    public class Quote : IAggregate<Guid>
    {
        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public decimal AmountRequired { get; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; }
    }
}