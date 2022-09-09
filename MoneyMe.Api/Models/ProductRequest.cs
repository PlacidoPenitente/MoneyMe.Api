using System;
using System.Collections.Generic;

namespace MoneyMe.Api.Models
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public decimal? InterestRate { get; set; }
        public int? MaximumDuration { get; set; }
        public int? MinimumDuration { get; set; }
        public List<Guid> FeeIds { get; set; }
        public string Rule { get; set; }
    }
}