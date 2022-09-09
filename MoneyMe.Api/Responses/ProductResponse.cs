using System;
using System.Collections.Generic;

namespace MoneyMe.Api.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Name { get; set; }
        public decimal? InterestRate { get; set; }
        public int? MaximumDuration { get; set; }
        public int? MinimumDuration { get; set; }
        public List<Guid> Fees { get; set; }
        public string Rule { get; set; }
    }
}