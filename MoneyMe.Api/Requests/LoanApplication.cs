using System;

namespace MoneyMe.Api.Requests
{
    public class LoanApplication
    {
        public Guid QuoteId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}