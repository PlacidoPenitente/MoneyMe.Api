using System;

namespace MoneyMe.Api.Responses
{
    public class QuoteResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public decimal LoanAmount { get; set; }
        public int Term { get; set; }
        public decimal Fee { get; set; }
        public decimal Interest { get; set; }
        public decimal MonthlyPayment { get; set; }
    }
}