using Microsoft.AspNetCore.Mvc;
using MoneyMe.Application.Contracts;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute]Guid id)
        {
            var customer = await _customerService.GetCustomerAsync(id);

            return Ok(customer);
        }
    }
}