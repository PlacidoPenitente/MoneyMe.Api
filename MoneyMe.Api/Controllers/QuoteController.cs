using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoneyMe.Api.Models;
using MoneyMe.Api.Responses;
using MoneyMe.Api.Validations;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using Serilog;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/quotes")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IEmailService _emailService;
        private readonly ISecurityService _securityService;
        private readonly ILogger _logger;
        private readonly Settings _settings;

        public QuoteController(
            IQuoteService quoteService,
            ICustomerService customerService,
            IEmailService emailService,
            ISecurityService securityService,
            ILogger logger,
            IOptions<Settings> options,
            IProductService productService)
        {
            _quoteService = quoteService;
            _customerService = customerService;
            _emailService = emailService;
            _securityService = securityService;
            _logger = logger;
            _settings = options.Value;
            _productService = productService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestQuoteAsync([FromBody] QuoteRequest quoteRequest)
        {
            if (!quoteRequest.IsValid(_settings))
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
                        MobileNumber = quoteRequest.Mobile,
                        EmailAddress = quoteRequest.Email
                    };

                    await _customerService.RegisterCustomerAsync(customerDto);
                }

                var quoteText = $"{customerDto.EmailAddress}|{quoteRequest.LoanAmount}|{quoteRequest.Term}";
                var encryptedQuoteText = _securityService.Encrypt(quoteText);
                var redirectUrl = $"http://localhost:4200/quote/{WebUtility.UrlEncode(encryptedQuoteText)}";

                await _emailService.SendMessageAsync(customerDto.EmailAddress, redirectUrl);

                return Ok(redirectUrl);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
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
                var loanAmount = decimal.Parse(quoteUrlSections[1]);
                var term = int.Parse(quoteUrlSections[2]);

                var customer = await _customerService.FindCustomerByEmailAsync(customerEmail);

                var response = new
                {
                    CustomerId = customer.Id,
                    LoanAmount = loanAmount,
                    Term = term,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
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
                    CustomerId = quoteDto.CustomerId,
                    ProductId = quoteDto.ProductId,
                    LoanAmount = quoteDto.LoanAmount,
                    Term = quoteDto.Term,
                    Interest = quoteDto.Interest,
                    Fee = quoteDto.Fee,
                    MonthlyPayment = quoteDto.MonthlyPayment
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Quote calculation failed.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuoteAsync(Guid id)
        {
            try
            {
                var quoteDto = await _quoteService.ReadQuoteAsync(id);
                var productDto = await _productService.ReadProductAsync(quoteDto.ProductId);
                var totalFee = 0m;

                foreach (var fee in productDto.Fees)
                {
                    if (fee.IsPercentage.Value)
                    {
                        totalFee += quoteDto.LoanAmount * fee.Amount.Value;
                    }
                    else
                    {
                        totalFee += fee.Amount.Value;
                    }
                }

                return Ok(new QuoteResponse
                {
                    Id = quoteDto.Id,
                    CustomerId = quoteDto.CustomerId,
                    ProductId = quoteDto.ProductId,
                    LoanAmount = quoteDto.LoanAmount,
                    Term = quoteDto.Term,
                    Interest = quoteDto.Interest,
                    Fee = totalFee,
                    MonthlyPayment = quoteDto.MonthlyPayment
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to retrieve quote.");
            }
        }
    }
}