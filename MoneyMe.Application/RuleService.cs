using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.Repositories;

namespace MoneyMe.Application
{
    public class RuleService : IRuleService
    {
        private readonly IRuleFactory _ruleFactory;
        private readonly IRuleRepository _ruleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RuleService(IRuleFactory ruleFactory, IRuleRepository ruleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _ruleFactory = ruleFactory;
            _ruleRepository = ruleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RuleDto> CreateRuleAsync(RuleDto ruleDto)
        {
            var rule = _ruleFactory.Create(ruleDto.Name);

            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () => await _ruleRepository.AddAsync(rule));
            }

            return _mapper.Map<RuleDto>(rule);
        }

        public async Task<RuleDto> ReadRuleAsync(Guid id)
        {
            var rule = await _ruleRepository.GetAsync(id);

            return _mapper.Map<RuleDto>(rule);
        }

        public async Task<IEnumerable<RuleDto>> ReadAllRulesAsync()
        {
            var rules = await _ruleRepository.GetAllAsync();

            return rules.Select(_mapper.Map<RuleDto>);
        }

        public async Task<RuleDto> UpdateRuleAsync(RuleDto ruleDto)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    var rule = await _ruleRepository.GetAsync(ruleDto.Id.Value);

                    rule.ChangeName(ruleDto.Name);

                    _ruleRepository.Update(rule);

                    return _mapper.Map<RuleDto>(rule);
                });
            }
        }

        public async Task DeleteRuleAsync(Guid id)
        {
            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () =>
                {
                    var rule = await _ruleRepository.GetAsync(id);

                    _ruleRepository.Remove(rule);
                });
            }
        }
    }
}