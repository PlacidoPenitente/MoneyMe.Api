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

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyAsync(Guid quoteId)
        {
            await _loanService.AppyAsync(quoteId);
            return Ok();
        }
    }
}