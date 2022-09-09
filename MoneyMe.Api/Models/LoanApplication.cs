using System;

namespace MoneyMe.Api.Models
{
    public class LoanApplication
    {
        public Guid QuoteId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}