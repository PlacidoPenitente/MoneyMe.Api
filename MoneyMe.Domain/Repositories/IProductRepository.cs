using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<IReadOnlyCollection<Product>> GetAllAsync();
        Task<Product> GetAsync(Guid id);
        Task RemoveAsync(Guid id);
    }
}