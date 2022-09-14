using MoneyMe.Domain.QuoteAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Factories
{
    public class QuoteFactory : IQuoteFactory
    {
        public Quote Create(Guid customerId, decimal loanAmount, int term, List<Guid> feeIds)
        {
            return new Quote(Guid.NewGuid(), DateTime.UtcNow, null, customerId, loanAmount, term, feeIds);
        }
    }
}