using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Product product)
        {
            await _context.AddAsync(product);
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products.Select(_mapper.Map<Product>).ToList();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(product => product.Id == id);
            return _mapper.Map<Product>(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == null) return;
            var productToBeRemoved = await GetAsync(id);

            _context.Remove(productToBeRemoved);
        }
    }
}