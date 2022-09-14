using System;

namespace MoneyMe.Api.Responses
{
    public class RuleResponse
    {
        public RuleResponse(Guid id, DateTime dateCreated, DateTime? dateModified, string name)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
            Name = name;
        }

        public Guid Id { get; }
        public DateTime DateCreated { get; }
        public DateTime? DateModified { get; }
        public string Name { get; }
    }
}