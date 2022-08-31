using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Requests;
using MoneyMe.Api.Responses;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using System.Net;
using System.Threading.Tasks;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [Route("api/quote")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly ICustomerService _customerService;
        private readonly IEmailService _emailService;
        private readonly ISecurityService _securityService;

        public QuoteController(
            IQuoteService quoteService,
            ICustomerService customerService,
            IEmailService emailService,
            ISecurityService securityService)
        {
            _quoteService = quoteService;
            _customerService = customerService;
            _emailService = emailService;
            _securityService = securityService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestQuoteAsync([FromBody] QuoteRequest quoteRequest)
        {
            var customerDto = await _customerService.FindCustomerByEmailAsync(quoteRequest.Email);

            if (customerDto == null)
            {
                customerDto = new CustomerDto
                {
                    Title = quoteRequest.Title,
                    FirstName = quoteRequest.FirstName,
                    LastName = quoteRequest.LastName,
                    DateOfBirth = quoteRequest.DateOfBirth,
                    Mobile = quoteRequest.Mobile,
                    Email = quoteRequest.Email
                };

                await _customerService.RegisterCustomerAsync(customerDto);
            }

            var quoteText = $"{customerDto.Email}|{quoteRequest.AmountRequired}|{quoteRequest.Term}";
            var encryptedQuoteText = _securityService.Encrypt(quoteText);
            var redirectUrl = $"http://localhost:4200/quote/{WebUtility.UrlEncode(encryptedQuoteText)}";

            await _emailService.SendMessageAsync(customerDto.Email, redirectUrl);

            return Ok(redirectUrl);
        }

        [HttpGet("continue/{encryptedQuoteUrl}")]
        public async Task<IActionResult> GetPartialQuote([FromRoute] string encryptedQuoteUrl)
        {
            var quoteUrl = _securityService.Decrypt(WebUtility.UrlDecode(encryptedQuoteUrl));

            var quoteUrlSections = quoteUrl.Split('|');
            var customerEmail = quoteUrlSections[0];
            var amountRequired = decimal.Parse(quoteUrlSections[1]);
            var term = int.Parse(quoteUrlSections[2]);

            var customer = await _customerService.FindCustomerByEmailAsync(customerEmail);

            var response = new
            {
                CustomerId = customer.Id,
                AmountRequired = amountRequired,
                Term = term,
            };

            return Ok(response);
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateQuoteAsync([FromBody] PartialQuoteDto partialQuoteDto)
        {
            var quoteDto = await _quoteService.CalculateAsync(partialQuoteDto);

            var response = new QuoteResponse
            {
                Id = quoteDto.Id,
                AmountRequired = quoteDto.LoanAmount,
                Terms = quoteDto.Terms,
                CustomerId = quoteDto.CustomerId
            };

            return Ok(response);
        }
    }
}