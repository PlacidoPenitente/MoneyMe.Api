using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class QuoteDto
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public decimal LoanAmount { get; set; }
        public int Term { get; set; }
        public decimal Interest { get; set; }
        public decimal Fee { get; set; }
        public decimal MonthlyPayment { get; set; }
    }
}