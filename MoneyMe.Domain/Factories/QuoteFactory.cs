using MoneyMe.Domain.QuoteAggregate;
using System;

namespace MoneyMe.Domain.Factories
{
    public class QuoteFactory : IQuoteFactory
    {
        public Quote Create(Guid customerId, decimal loanAmount, int term)
        {
            return new Quote(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, customerId, loanAmount, term);
        }
    }
}