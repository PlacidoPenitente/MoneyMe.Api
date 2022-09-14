using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MoneyMe.Api.Requests;
using MoneyMe.Api.Responses;
using MoneyMe.Api.Validations;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using Serilog;

namespace MoneyMe.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/rules")]
    public class RuleController : ControllerBase
    {
        private readonly IRuleService _ruleService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RuleController(IRuleService ruleService, IMapper mapper, ILogger logger)
        {
            _ruleService = ruleService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RuleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateRuleAsync([FromBody] RuleRequest ruleRequest)
        {
            try
            {
                if (ruleRequest == null || !ruleRequest.IsValid())
                {
                    return BadRequest();
                }

                var ruleDto = new RuleDto
                {
                    Name = ruleRequest.Name
                };

                var createdRuleDto = await _ruleService.CreateRuleAsync(ruleDto);
                var rule = _mapper.Map<RuleResponse>(createdRuleDto);

                return Created($"{Request.GetEncodedUrl()}/{rule.Id}", rule);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create rule.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RuleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadRuleAsync([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var ruleId))
                {
                    return BadRequest();
                }

                var rule = await _ruleService.ReadRuleAsync(ruleId);

                if (rule == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<RuleResponse>(rule));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read rule.");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RuleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadAllRulesAsync()
        {
            try
            {
                var rules = await _ruleService.ReadAllRulesAsync();

                return Ok(rules.Select(_mapper.Map<RuleResponse>));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to read rules.");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RuleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRuleAsync([FromRoute] Guid id, [FromBody] RuleRequest ruleRequest)
        {
            try
            {
                if (ruleRequest == null || !ruleRequest.IsValid())
                {
                    return BadRequest();
                }

                var ruleDto = new RuleDto(id)
                {
                    Name = ruleRequest.Name
                };

                var updatedRuleDto = await _ruleService.UpdateRuleAsync(ruleDto);

                return Ok(_mapper.Map<RuleResponse>(updatedRuleDto));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update rule.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRuleAsync([FromRoute] string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var ruleId))
                {
                    return BadRequest();
                }

                await _ruleService.DeleteRuleAsync(ruleId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete rule.");
            }
        }
    }
}