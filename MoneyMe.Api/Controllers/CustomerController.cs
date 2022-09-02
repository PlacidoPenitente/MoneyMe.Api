using Microsoft.AspNetCore.Mvc;
using MoneyMe.Application.Contracts;
using Serilog.Core;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly Logger _logger;

        public CustomerController(ICustomerService customerService, Logger logger)
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
                _logger.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to retrieve customer.");
            }
        }
    }
}