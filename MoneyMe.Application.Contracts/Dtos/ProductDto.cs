using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MinimumDuration { get; set; }
        public int MaximumDuration { get; set; }
    }
}