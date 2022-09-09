using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Models;
using MoneyMe.Api.Validations;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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
        [ProducesResponseType(typeof(Fee), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateFeeAsync([FromBody] Fee feeRequest)
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
                    Amount = feeRequest.Amount
                };

                var fee = await _feeService.CreateFeeAsync(feeDto);

                return Ok(_mapper.Map<Fee>(fee));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to create fee.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Fee), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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

                return Ok(_mapper.Map<Fee>(fee));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to get fee.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Fee>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ReadAllFeesAsync()
        {
            try
            {
                var fees = await _feeService.ReadAllFeesAsync();

                return Ok(fees.Select(_mapper.Map<Fee>));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to get fee.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Fee), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateFeeAsync([FromRoute] string id, [FromBody] Fee feeRequest)
        {
            try
            {
                if (feeRequest == null || feeRequest.IsValid() || !Guid.TryParse(id, out var feeId))
                {
                    return BadRequest();
                }

                var feeDto = new FeeDto(feeId)
                {
                    Name = feeRequest.Name,
                    Amount = feeRequest.Amount
                };

                var fee = await _feeService.UpdateFeeAsync(feeDto);

                return Ok(_mapper.Map<Fee>(fee));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to update fee.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
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
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to delete fee.");
            }
        }
    }
}