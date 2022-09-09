using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Models;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using Serilog;
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
        private readonly ILogger _logger;

        public ProductController(IProductService productService, ILogger logger)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync([FromRoute] Guid id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to get product.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest createProductRequest)
        {
            try
            {
                var productDto = new ProductDto
                {
                };

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to create product.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] ProductRequest createProductRequest)
        {
            try
            {
                var productDto = new ProductDto(id)
                {
                };


                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to update product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to delete product.");
            }
        }
    }
}