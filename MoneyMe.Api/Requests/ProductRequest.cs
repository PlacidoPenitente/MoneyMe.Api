using System.Collections.Generic;
using System;

namespace MoneyMe.Api.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public decimal? InterestRate { get; set; }
        public int? MinimumDuration { get; set; }
        public int? MaximumDuration { get; set; }
        public string Rule { get; set; }
    }
}