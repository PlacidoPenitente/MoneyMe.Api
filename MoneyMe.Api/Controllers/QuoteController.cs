using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Requests;
using MoneyMe.Application.Contracts;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [Route("api/quote")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpPost("request")]
        public IActionResult RequestQuote([FromBody] QuoteRequest quoteRequest)
        {
            return Ok();
        }
    }
}