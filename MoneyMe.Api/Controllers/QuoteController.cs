using Microsoft.AspNetCore.Mvc;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [Route("api/quote")]
    public class QuoteController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetQuote()
        {
            return Ok("hello");
        }
    }
}