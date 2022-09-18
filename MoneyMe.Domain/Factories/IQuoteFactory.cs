using System;
using MoneyMe.Domain.QuoteAggregate;

namespace MoneyMe.Domain.Factories
{
    public interface IQuoteFactory
    {
        Quote Create(Guid customerId, decimal loanAmount, int term, Guid productId);
    }
}