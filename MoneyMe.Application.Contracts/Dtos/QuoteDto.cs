using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class QuoteDto
    {
        public Guid Id { get; set; }
        public decimal AmountRequired { get; set; }
        public int Terms { get; set; }
        public Guid CustomerId { get; set; }
    }
}