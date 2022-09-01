using System;

namespace MoneyMe.Api.Responses
{
    public class QuoteResponse
    {
        public Guid Id { get; set; }
        public decimal AmountRequired { get; set; }
        public int Terms { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Monthly { get; set; }
        public decimal Fee { get; set; }
        public Guid productId { get; set; }
    }
}