using MoneyMe.Domain.QuoteAggregate;
using System;

namespace MoneyMe.Domain.Factories
{
    public interface IQuoteFactory
    {
        Quote Create(Guid customerId, decimal loanAmount, int term);
    }
}