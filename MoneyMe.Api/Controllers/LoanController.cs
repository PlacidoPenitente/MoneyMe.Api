using Microsoft.AspNetCore.Mvc;
using MoneyMe.Application.Contracts;
using System;
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

        public LoanController(ILoanService loanService, IEmailService emailService, ICustomerService customerService)
        {
            _loanService = loanService;
            _emailService = emailService;
            _customerService = customerService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyAsync(Guid quoteId)
        {
            var loanDto = await _loanService.ApplyAsync(quoteId);
            var customer = await _customerService.GetCustomerAsync(loanDto.CustomerId);

            await _emailService.SendMessageAsync(customer.Email, $"{loanDto.Id}");
            
            return Ok();
        }
    }
}