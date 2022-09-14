using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            Id = null;
            DateCreated = null;
            DateModified = null;
        }

        public ProductDto(Guid id)
        {
            Id = id;
            DateCreated = null;
            DateModified = null;
        }

        public ProductDto(Guid id, DateTime dateCreated, DateTime? dateModified)
        {
            Id = id;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public Guid? Id { get; }
        public DateTime? DateCreated { get; }
        public DateTime? DateModified { get; }
        public string Name { get; set; }
        public decimal? InterestRate { get; set; }
        public int? MaximumDuration { get; set; }
        public int? MinimumDuration { get; set; }
        public Guid RuleId { get; set; }
    }
}