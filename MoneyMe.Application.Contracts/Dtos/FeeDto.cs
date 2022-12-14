using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class FeeDto
    {
        public FeeDto()
        {
            Id = null;
            DateCreated = null;
            DateModified = null;
        }

        public FeeDto(Guid id)
        {
            Id = id;
            DateCreated = null;
            DateModified = null;
        }

        public FeeDto(Guid id, DateTime dateCreated, DateTime? dateModified)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public Guid? Id { get; }
        public DateTime? DateCreated { get; }
        public DateTime? DateModified { get; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsPercentage { get; set; }
    }
}