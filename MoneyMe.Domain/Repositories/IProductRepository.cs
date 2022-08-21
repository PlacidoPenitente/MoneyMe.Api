using MoneyMe.Domain.ProductAggregate;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface IProductRepository
    {
        public Task<Product> GetAsync(Guid id);
        public Task<Product> FindByNumberOfTerms(int terms);
    }
}