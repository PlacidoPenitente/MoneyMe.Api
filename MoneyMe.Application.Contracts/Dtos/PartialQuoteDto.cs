using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class PartialQuoteDto
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public decimal LoanAmount { get; set; }
        public int Term { get; set; }
    }
}