using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Models;
using MoneyMe.Application.Contracts;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [Route("api/loan")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly ICustomerService _customerService;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public LoanController(ILoanService loanService, IEmailService emailService, ICustomerService customerService, ILogger logger)
        {
            _loanService = loanService;
            _emailService = emailService;
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyAsync([FromBody] LoanApplication loanApplication)
        {
            if (loanApplication == null)
                return BadRequest();

            try
            {
                var customer = await _customerService.GetCustomerAsync(loanApplication.CustomerId);
                var loanDto = await _loanService.ApplyAsync(loanApplication.QuoteId, loanApplication.ProductId);

                await _emailService.SendMessageAsync(customer.EmailAddress, $"{loanDto.Id}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to process your application.");
            }
        }
    }
}