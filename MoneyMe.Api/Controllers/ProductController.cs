using Microsoft.AspNetCore.Mvc;
using MoneyMe.Application.Contracts;
using Serilog.Core;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly Logger _logger;

        public ProductController(IProductService productService, Logger logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to retrieve products.");
            }
        }
    }
}