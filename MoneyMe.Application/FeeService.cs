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
    public class FeeService : IFeeService
    {
        private readonly IFeeFactory _feeFactory;
        private readonly IFeeRepository _feeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeeService(IFeeFactory feeFactory, IFeeRepository feeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _feeFactory = feeFactory;
            _feeRepository = feeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeeDto> CreateFeeAsync(FeeDto feeDto)
        {
            var fee = _feeFactory.Create(feeDto.Name, feeDto.Amount.Value);

            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () => await _feeRepository.AddAsync(fee));
            }

            return _mapper.Map<FeeDto>(fee);
        }

        public async Task<FeeDto> ReadFeeAsync(Guid id)
        {
            var fee = await _feeRepository.GetAsync(id);

            return _mapper.Map<FeeDto>(fee);
        }

        public async Task<IEnumerable<FeeDto>> ReadAllFeesAsync()
        {
            var fees = await _feeRepository.GetAllAsync();

            return fees.Select(_mapper.Map<FeeDto>);
        }

        public async Task<FeeDto> UpdateFeeAsync(FeeDto feeDto)
        {
            using (_unitOfWork)
            {
                return await _unitOfWork.ExecuteAsync(async () =>
                {
                    var fee = await _feeRepository.GetAsync(feeDto.Id);

                    fee.ChangeName(feeDto.Name);
                    fee.ChangeAmount(feeDto.Amount);

                    _feeRepository.Update(fee);

                    return _mapper.Map<FeeDto>(fee);
                });
            }
        }

        public async Task DeleteFeeAsync(Guid id)
        {
            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () =>
                {
                    var fee = await _feeRepository.GetAsync(id);

                    _feeRepository.Remove(fee);
                });
            }
        }
    }
}