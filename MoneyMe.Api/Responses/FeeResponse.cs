using System;

namespace MoneyMe.Api.Responses
{
    public class FeeResponse
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}