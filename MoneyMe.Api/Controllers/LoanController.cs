using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Requests;
using MoneyMe.Application.Contracts;
using Serilog.Core;
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
        private readonly Logger _logger;

        public LoanController(ILoanService loanService, IEmailService emailService, ICustomerService customerService, Logger logger)
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

                await _emailService.SendMessageAsync(customer.Email, $"{loanDto.Id}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to process your application.");
            }
        }
    }
}