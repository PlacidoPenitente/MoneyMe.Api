using System;
using MoneyMe.Domain.QuoteAggregate;

namespace MoneyMe.Domain.Factories
{
    public class QuoteFactory : IQuoteFactory
    {
        public Quote Create(Guid customerId, decimal loanAmount, int term, Guid productId)
        {
            return new Quote(Guid.NewGuid(), DateTime.UtcNow, null, customerId, loanAmount, term, productId);
        }
    }
}