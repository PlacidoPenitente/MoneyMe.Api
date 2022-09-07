using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class FeeDto
    {
        public FeeDto()
        {
            Id = Guid.NewGuid();
        }

        public FeeDto(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
    }
}