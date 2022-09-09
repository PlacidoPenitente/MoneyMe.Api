﻿using System;
using System.Collections.Generic;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class ProductDto
    {
        public ProductDto()
        {
            Id = Guid.NewGuid();
        }

        public ProductDto(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public string Name { get; set; }
        public int? MaximumDuration { get; set; }
        public int? MinimumDuration { get; set; }
        public List<Guid> FeeIds { get; set; }
        public string Rule { get; set; }
    }
}