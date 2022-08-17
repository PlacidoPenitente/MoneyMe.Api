using Microsoft.AspNetCore.Mvc;
using MoneyMe.Application.Contracts;

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
        public IActionResult Apply()
        {
            return Ok("hello");
        }
    }
}