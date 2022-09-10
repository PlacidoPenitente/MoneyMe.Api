using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Requests;
using MoneyMe.Api.Responses;
using MoneyMe.Api.Validations;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using Serilog;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductController(IProductService productService, IMapper mapper, ILogger logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest productRequest)
        {
            try
            {
                if (productRequest == null || !productRequest.IsValid())
                {
                    return BadRequest();
                }

                var productDto = new ProductDto
                {
                    Name = productRequest.Name,
                    InterestRate = productRequest.InterestRate,
                    MinimumDuration = productRequest.MinimumDuration,
                    MaximumDuration = productRequest.MaximumDuration,
                    Rule = productRequest.Rule
                };

                var createdProductDto = await _productService.CreateProductAsync(productDto);
                var product = _mapper.Map<ProductResponse>(createdProductDto);

                return Created($"{Request.GetEncodedUrl()}/{product.Id}", product);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create product.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadProductAsync([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var productId))
                {
                    return BadRequest();
                }

                var product = await _productService.ReadProductAsync(productId);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ProductResponse>(product));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read product.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadAllProductsAsync()
        {
            try
            {
                var products = await _productService.ReadAllProductsAsync();

                return Ok(products.Select(_mapper.Map<ProductResponse>));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read products.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] ProductRequest productRequest)
        {
            try
            {
                if (productRequest == null || !productRequest.IsValid())
                {
                    return BadRequest();
                }

                var productDto = new ProductDto(id)
                {
                    Name = productRequest.Name,
                    InterestRate = productRequest.InterestRate,
                    MinimumDuration = productRequest.MinimumDuration,
                    MaximumDuration = productRequest.MaximumDuration,
                    Rule = productRequest.Rule
                };

                var updatedProductDto = await _productService.UpdateProductAsync(productDto);

                return Ok(_mapper.Map<ProductResponse>(updatedProductDto));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update product.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var productId))
                {
                    return BadRequest();
                }

                await _productService.DeleteProductAsync(productId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete product.");
            }
        }
    }
}