using System;
using System.Collections.Generic;

namespace MoneyMe.Api.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public decimal? InterestRate { get; set; }
        public int? MinimumDuration { get; set; }
        public int? MaximumDuration { get; set; }
        public Guid RuleId { get; set; }
        public List<Guid> FeeIds { get; set; }
    }
}