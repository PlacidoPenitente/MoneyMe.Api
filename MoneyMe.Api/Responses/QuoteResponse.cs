using System;

namespace MoneyMe.Api.Responses
{
    public class QuoteResponse
    {
        public Guid Id { get; set; }
        public decimal AmountRequired { get; set; }
        public int Terms { get; set; }
        public Guid CustomerId { get; set; }
    }
}