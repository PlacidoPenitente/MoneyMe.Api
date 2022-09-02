using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoneyMe.Api.Requests;
using MoneyMe.Api.Responses;
using MoneyMe.Api.Validations;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using Serilog.Core;
using System;
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
        private readonly MoneyMeSettings _moneyMeSettings;
        private readonly Logger _logger;

        public QuoteController(
            IQuoteService quoteService,
            ICustomerService customerService,
            IEmailService emailService,
            ISecurityService securityService,
            IOptions<MoneyMeSettings> options,
            Logger logger)
        {
            _quoteService = quoteService;
            _customerService = customerService;
            _emailService = emailService;
            _securityService = securityService;
            _moneyMeSettings = options.Value;
            _logger = logger;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestQuoteAsync([FromBody] QuoteRequest quoteRequest)
        {
            if (!quoteRequest.IsValid(_moneyMeSettings))
            {
                return BadRequest();
            }

            try
            {
                var customerDto = await _customerService.FindCustomerByEmailAsync(quoteRequest.Email);

                if (customerDto == null)
                {
                    customerDto = new CustomerDto
                    {
                        Title = quoteRequest.Title,
                        FirstName = quoteRequest.FirstName,
                        LastName = quoteRequest.LastName,
                        DateOfBirth = quoteRequest.DateOfBirth.Value,
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
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to generate quote url.");
            }
        }

        [HttpPost("continue")]
        public async Task<IActionResult> GetPartialQuote([FromBody] ContinueQuoteRequest continueQuoteRequest)
        {
            if (continueQuoteRequest == null || string.IsNullOrWhiteSpace(continueQuoteRequest.EncryptedQuoteUrl))
                return BadRequest();

            try
            {
                var quoteUrl = _securityService.Decrypt(WebUtility.UrlDecode(continueQuoteRequest.EncryptedQuoteUrl));

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
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to retrieve quote request.");
            }
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateQuoteAsync([FromBody] PartialQuoteDto partialQuoteDto)
        {
            try
            {
                var quoteDto = await _quoteService.CalculateAsync(partialQuoteDto);

                var response = new QuoteResponse
                {
                    Id = quoteDto.Id,
                    AmountRequired = quoteDto.LoanAmount,
                    Terms = quoteDto.Terms,
                    CustomerId = quoteDto.CustomerId,
                    Monthly = quoteDto.MonthlyPayment
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Quote calculation failed.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuoteAsync(Guid id)
        {
            try
            {
                var quote = await _quoteService.GetQuoteAsync(id);

                return Ok(new QuoteResponse
                {
                    Id = quote.Id,
                    AmountRequired = quote.LoanAmount,
                    CustomerId = quote.CustomerId,
                    Monthly = quote.MonthlyPayment,
                    Fee = 0,
                    Terms = quote.Terms
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to retrieve quote.");
            }
        }
    }
}