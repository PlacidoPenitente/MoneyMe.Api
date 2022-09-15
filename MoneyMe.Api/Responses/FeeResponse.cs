using System;

namespace MoneyMe.Api.Responses
{
    public class FeeResponse
    {
        public FeeResponse(
            Guid id,
            DateTime dateCreated,
            DateTime? dateModified,
            string name,
            decimal amount,
            bool isPercentage)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
            Name = name;
            Amount = amount;
            IsPercentage = isPercentage;
        }

        public Guid Id { get; }
        public DateTime DateCreated { get; }
        public DateTime? DateModified { get; }
        public string Name { get; }
        public decimal Amount { get; }
        public bool IsPercentage { get; }
    }
}