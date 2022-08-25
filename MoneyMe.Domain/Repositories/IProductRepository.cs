using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.ProductAggregate;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product> GetAsync(Guid id);
        public Task<Product> FindByNumberOfTermsAsync(int terms);
        Task RemoveAsync(Guid id);
    }
}