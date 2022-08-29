using MoneyMe.Application.Contracts.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IProductService
    {
        Task<IReadOnlyCollection<ProductDto>> GetAllProductsAsync();
    }
}