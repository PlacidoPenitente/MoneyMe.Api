using Microsoft.EntityFrameworkCore;
using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.Repositories;
using MoneyMe.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.AddAsync(product);
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _context.Products.SingleOrDefaultAsync(product => product.Id == id);
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == null) return;
            var productToBeRemoved = await GetAsync(id);

            _context.Remove(productToBeRemoved);
        }
    }
}