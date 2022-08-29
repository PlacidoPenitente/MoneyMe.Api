using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class PartialQuoteDto
    {
        public decimal AmountRequired { get; set; }
        public int Term { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}