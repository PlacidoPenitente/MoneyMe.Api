using System;

namespace MoneyMe.Api.Responses
{
    public class FeeResponse
    {
        public FeeResponse(Guid id, DateTime dateCreated, DateTime? dateModified, string name, decimal amount)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
            Name = name;
            Amount = amount;
        }

        public Guid Id { get; }
        public DateTime DateCreated { get; }
        public DateTime? DateModified { get; }
        public string Name { get; }
        public decimal Amount { get; }
    }
}