using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IRuleService
    {
        Task<RuleDto> CreateRuleAsync(RuleDto feeDto);
        Task<RuleDto> ReadRuleAsync(Guid id);
        Task<IEnumerable<RuleDto>> ReadAllRulesAsync();
        Task<RuleDto> UpdateRuleAsync(RuleDto feeDto);
        Task DeleteRuleAsync(Guid id);
    }
}