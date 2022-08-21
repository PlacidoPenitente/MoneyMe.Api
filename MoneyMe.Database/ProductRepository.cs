using MoneyMe.Domain.ProductAggregate;
using MoneyMe.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product> FindByNumberOfTerms(int terms)
        {
            return Task.FromResult(new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product", 0.2m));
        }

        public Task<Product> GetAsync(Guid id)
        {
            return Task.FromResult(new Product(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, "Product", 0.2m));
        }
    }
}