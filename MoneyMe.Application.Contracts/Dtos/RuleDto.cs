using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class RuleDto
    {
        public RuleDto()
        {
            Id = null;
            DateCreated = null;
            DateModified = null;
        }

        public RuleDto(Guid id)
        {
            Id = id;
            DateCreated = null;
            DateModified = null;
        }

        public RuleDto(Guid id, DateTime dateCreated, DateTime? dateModified)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public Guid? Id { get; }
        public DateTime? DateCreated { get; }
        public DateTime? DateModified { get; }
        public string Name { get; set; }
    }
}