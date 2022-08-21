using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product> FindByNumberOfTerms(int terms)
        {
            return Task.FromResult(new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product", 0.2m, terms));
        }

        public Task<Product> GetAsync(Guid id)
        {
            return Task.FromResult(new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product", 0.2m, 6));
        }
    }
}