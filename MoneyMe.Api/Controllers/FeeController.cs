using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Requests;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using Serilog.Core;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [Route("api/fee")]
    public class FeeController : ControllerBase
    {
        private readonly IFeeService _feeService;
        private readonly Logger _logger;

        public FeeController(IFeeService feeService, Logger logger)
        {
            _feeService = feeService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeeAsync([FromRoute] Guid id)
        {
            try
            {
                var fee = await _feeService.ReadFeeAsync(id);

                return Ok(fee);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to get fee.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeeAsync([FromBody] FeeRequest createFeeRequest)
        {
            try
            {
                var feeDto = new FeeDto
                {
                    Name = createFeeRequest.Name,
                    Amount = createFeeRequest.Amount
                };

                var fee = await _feeService.CreateFeeAsync(feeDto);

                return Ok(fee);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to create fee.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeeAsync([FromRoute] Guid id, [FromBody] FeeRequest createFeeRequest)
        {
            try
            {
                var feeDto = new FeeDto(id)
                {
                    Name = createFeeRequest.Name,
                    Amount = createFeeRequest.Amount
                };

                var fee = await _feeService.UpdateFeeAsync(feeDto);

                return Ok(fee);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to update fee.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeeAsync([FromRoute] Guid id)
        {
            try
            {
                await _feeService.DeleteFeeAsync(id);

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