using MoneyMe.Domain.QuoteAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Factories
{
    public interface IQuoteFactory
    {
        Quote Create(Guid customerId, decimal loanAmount, int term, List<Guid> feeIds);
    }
}