using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task<ProductDto> ReadProductAsync(Guid id);
        Task<IEnumerable<ProductDto>> ReadAllProductsAsync();
        Task<ProductDto> UpdateProductAsync(ProductDto productDto);
        Task DeleteProductAsync(Guid id);
    }
}