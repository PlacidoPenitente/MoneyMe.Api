﻿using AutoMapper;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;

namespace MoneyMe.Infrastructure.Repositories
{
    public sealed class ProductRepository : Repository<Product, Database.Models.Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}