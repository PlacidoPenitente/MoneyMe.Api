using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoneyMe.Api.Requests;
using MoneyMe.Api.Responses;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Infrastructure.Services;
using System;
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
            var customerDto = await _customerService.FindCustomerByEmail(quoteRequest.Email);

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

                await _customerService.RegisterCustomer(customerDto);
            }

            var quoteUrl = $"{customerDto.Id}|{quoteRequest.AmountRequired}|{quoteRequest.Terms}";

            var encryptedQuoteUrl = _securityService.Encrypt(quoteUrl);

            var redirectUrl = $"https://localhost:4200/quote/partial/{encryptedQuoteUrl}";

            await _emailService.SendRedirectUrlAsync(customerDto.Email, redirectUrl);

            return Ok();
        }

        [HttpPost("continue")]
        public IActionResult GetPartialQuote(string encryptedQuoteUrl)
        {
            var quoteUrl = _securityService.Decrypt(encryptedQuoteUrl);

            var quoteUrlSections = quoteUrl.Split('|');
            var customerId = Guid.Parse(quoteUrlSections[0]);
            var amountRequired = decimal.Parse(quoteUrlSections[1]);
            var terms = int.Parse(quoteUrlSections[2]);

            var response = new QuoteResponse
            {
                CustomerId = customerId,
                AmountRequired = amountRequired,
                Terms = terms,
            };

            return Ok(response);
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateQuoteAsync(PartialQuoteDto partialQuoteDto)
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