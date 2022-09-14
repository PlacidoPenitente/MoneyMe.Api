﻿using System;
using System.Collections.Generic;

namespace MoneyMe.Api.Responses
{
    public class ProductResponse
    {
        public ProductResponse(
            Guid id,
            DateTime dateAdded,
            DateTime? dateModified,
            string name,
            decimal interestRate,
            int maximumDuration,
            int minimumDuration,
            Guid ruleId)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            Name = name;
            InterestRate = interestRate;
            MaximumDuration = maximumDuration;
            MinimumDuration = minimumDuration;
            RuleId = ruleId;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime? DateModified { get; }
        public string Name { get; }
        public decimal InterestRate { get; }
        public int MaximumDuration { get; }
        public int MinimumDuration { get; }
        public Guid RuleId { get; }
    }
}