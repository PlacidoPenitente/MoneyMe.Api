using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Requests;
using MoneyMe.Api.Responses;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
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

        public QuoteController(IQuoteService quoteService, ICustomerService customerService)
        {
            _quoteService = quoteService;
            _customerService = customerService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestQuote([FromBody] QuoteRequest quoteRequest)
        {
            var customer = new CustomerDto
            {
                Title = quoteRequest.Title,
                FirstName = quoteRequest.FirstName,
                LastName = quoteRequest.LastName,
                DateOfBirth = quoteRequest.DateOfBirth,
                Mobile = quoteRequest.Mobile,
                Email = quoteRequest.Email
            };

            var registeredCustomer = await _customerService.RegisterCustomer(customer);

            var quoteDto = new QuoteDto
            {
                AmountRequired = quoteRequest.AmountRequired,
                Terms = quoteRequest.Term,
                CustomerId = registeredCustomer.Id
            };

            var redirectUrl = await _quoteService.RequestQuoteAsync(quoteDto);

            return Ok(redirectUrl);
        }

        [HttpPost("continue")]
        public async Task<IActionResult> ContinueQuote(Guid quoteId)
        {
            var quoteDto = await _quoteService.ContinueQuoteAsync(quoteId);

            var response = new QuoteResponse
            {
                Id = quoteDto.Id,
                AmountRequired = quoteDto.AmountRequired,
                Terms = quoteDto.Terms,
                CustomerId = quoteDto.CustomerId
            };

            return Ok(response);
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateQuote(Guid quoteId, Guid productId)
        {
            var quoteDto = await _quoteService.CalculateAsync(quoteId, productId);

            var response = new QuoteResponse
            {
                Id = quoteDto.Id,
                AmountRequired = quoteDto.AmountRequired,
                Terms = quoteDto.Terms,
                CustomerId = quoteDto.CustomerId
            };

            return Ok(response);
        }
    }
}