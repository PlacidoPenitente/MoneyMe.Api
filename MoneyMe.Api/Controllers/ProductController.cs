using Microsoft.AspNetCore.Mvc;
using MoneyMe.Application.Contracts;
using System.Threading.Tasks;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
    }
}