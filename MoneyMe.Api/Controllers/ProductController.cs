using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Requests;
using MoneyMe.Api.Responses.ProductResponse;
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
        private readonly IFeeService _feeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductController(IProductService productService, IFeeService feeService, IMapper mapper, ILogger logger)
        {
            _productService = productService;
            _feeService = feeService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest productRequest)
        {
            try
            {
                if (productRequest == null || !productRequest.IsValid())
                {
                    return BadRequest();
                }

                var fees = new List<FeeDto>();

                foreach (var feeId in productRequest.FeeIds)
                {
                    var fee = await _feeService.ReadFeeAsync(feeId);

                    if (fee != null)
                        fees.Add(fee);
                }

                var productDto = new ProductDto
                {
                    Name = productRequest.Name,
                    InterestRate = productRequest.InterestRate,
                    MinimumDuration = productRequest.MinimumDuration,
                    MaximumDuration = productRequest.MaximumDuration,
                    RuleId = productRequest.RuleId,
                    Fees = fees
                };

                var createdProductDto = await _productService.CreateProductAsync(productDto);
                var product = _mapper.Map<Product>(createdProductDto);

                return Created($"{Request.GetEncodedUrl()}/{product.Id}", product);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create product.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
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

                return Ok(_mapper.Map<Product>(product));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read product.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadAllProductsAsync()
        {
            try
            {
                var products = await _productService.ReadAllProductsAsync();

                return Ok(products.Select(_mapper.Map<Product>));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read products.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] ProductRequest productRequest)
        {
            try
            {
                if (productRequest == null)
                {
                    return BadRequest();
                }

                var fees = new List<FeeDto>();

                foreach (var feeId in productRequest.FeeIds)
                {
                    var fee = await _feeService.ReadFeeAsync(feeId);

                    if (fee == null)
                        return BadRequest();

                    fees.Add(fee);
                }

                var productDto = new ProductDto(id)
                {
                    Name = productRequest.Name,
                    InterestRate = productRequest.InterestRate,
                    MinimumDuration = productRequest.MinimumDuration,
                    MaximumDuration = productRequest.MaximumDuration,
                    RuleId = productRequest.RuleId,
                    Fees = fees
                };

                var updatedProductDto = await _productService.UpdateProductAsync(productDto);

                return Ok(_mapper.Map<Product>(updatedProductDto));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update product.");
            }
        }

        [HttpPatch("{id}/fees/add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddFeeToProductAsync([FromRoute] Guid id, Guid? feeId)
        {
            try
            {
                if (!feeId.HasValue)
                {
                    return BadRequest();
                }

                await _productService.AddFeeToProductAsync(id, feeId.Value);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add fee.");
            }
        }

        [HttpPatch("{id}/fees/remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveFeeFromProductAsync([FromRoute] Guid id, Guid? feeId)
        {
            try
            {
                if (!feeId.HasValue)
                {
                    return BadRequest();
                }

                await _productService.RemoveFeeFromProductAsync(id, feeId.Value);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to remove fee.");
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