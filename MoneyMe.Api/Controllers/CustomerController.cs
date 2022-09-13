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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public CustomerController(ICustomerService customerService, ILogger logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute] Guid id)
        {
            try
            {
                var customer = await _customerService.GetCustomerAsync(id);
                
                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to retrieve customer.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerRequest updateCustomerRequest)
        {
            try
            {
                var customerDto = await _customerService.GetCustomerAsync(updateCustomerRequest.Id);

                if (customerDto == null)
                {
                    return NotFound();
                }

                var updatedCustomer = new CustomerDto(customerDto.Id)
                {
                    Title = updateCustomerRequest.Title,
                    FirstName = updateCustomerRequest.FirstName,
                    LastName = updateCustomerRequest.LastName,
                    EmailAddress = updateCustomerRequest.Email,
                    MobileNumber = customerDto.MobileNumber
                };

                var updatedCustomerDto = await _customerService.UpdateCustomerAsync(updatedCustomer);

                return Ok(updatedCustomerDto);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to retrieve customer.");
            }
        }
    }
}