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
    [Route("api/v{version:apiVersion}/fees")]
    public class FeeController : ControllerBase
    {
        private readonly IFeeService _feeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FeeController(IFeeService feeService, IMapper mapper, ILogger logger)
        {
            _feeService = feeService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FeeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFeeAsync([FromBody] FeeRequest feeRequest)
        {
            try
            {
                if (feeRequest == null || !feeRequest.IsValid())
                {
                    return BadRequest();
                }

                var feeDto = new FeeDto
                {
                    Name = feeRequest.Name,
                    Amount = feeRequest.Amount,
                    IsPercentage = feeRequest.IsPercentage
                };

                var createdFeeDto = await _feeService.CreateFeeAsync(feeDto);
                var fee = _mapper.Map<FeeResponse>(createdFeeDto);

                return Created($"{Request.GetEncodedUrl()}/{fee.Id}", fee);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create fee.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadFeeAsync([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var feeId))
                {
                    return BadRequest();
                }

                var fee = await _feeService.ReadFeeAsync(feeId);

                if (fee == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<FeeResponse>(fee));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read fee.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadAllFeesAsync()
        {
            try
            {
                var fees = await _feeService.ReadAllFeesAsync();

                return Ok(fees.Select(_mapper.Map<FeeResponse>));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read fees.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FeeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFeeAsync([FromRoute] Guid id, [FromBody] FeeRequest feeRequest)
        {
            try
            {
                if (feeRequest == null)
                {
                    return BadRequest();
                }

                var feeDto = new FeeDto(id)
                {
                    Name = feeRequest.Name,
                    Amount = feeRequest.Amount,
                    IsPercentage = feeRequest.IsPercentage
                };

                var updatedFeeDto = await _feeService.UpdateFeeAsync(feeDto);

                return Ok(_mapper.Map<FeeResponse>(updatedFeeDto));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update fee.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFeeAsync([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var feeId))
                {
                    return BadRequest();
                }

                await _feeService.DeleteFeeAsync(feeId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete fee.");
            }
        }
    }
}