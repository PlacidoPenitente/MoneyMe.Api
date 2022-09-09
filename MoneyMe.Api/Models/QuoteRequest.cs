using System;

namespace MoneyMe.Api.Models
{
    public class QuoteRequest
    {
        public decimal LoanAmount { get; set; }
        public int Term { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}