using MoneyMe.Application.Contracts;
using MoneyMe.Application.Contracts.Dtos;
using MoneyMe.Domain;
using MoneyMe.Domain.Factories;
using MoneyMe.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application
{
    public class FeeService : IFeeService
    {
        private readonly IFeeFactory _feeFactory;
        private readonly IFeeRepository _feeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FeeService(IFeeFactory feeFactory, IFeeRepository feeRepository, IUnitOfWork unitOfWork)
        {
            _feeFactory = feeFactory;
            _feeRepository = feeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FeeDto> CreateFeeAsync(FeeDto feeDto)
        {
            var fee = _feeFactory.Create(feeDto.Name, feeDto.Amount.Value);

            using (_unitOfWork)
            {
                await _unitOfWork.ExecuteAsync(async () => await _feeRepository.AddAsync(fee));
            }

            return new FeeDto(fee.Id)
            {
                DateAdded = fee.DateAdded,
                DateModified = fee.DateModified,
                Name = fee.Name,
                Amount = fee.Amount
            };
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

        public async Task<FeeDto> ReadFeeAsync(Guid id)
        {
            var fee = await _feeRepository.GetAsync(id);

            return new FeeDto(fee.Id)
            {
                DateAdded = fee.DateAdded,
                DateModified = fee.DateModified,
                Name = fee.Name,
                Amount = fee.Amount
            };
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

                    return new FeeDto(fee.Id)
                    {
                        DateAdded = fee.DateAdded,
                        DateModified = fee.DateModified,
                        Name = fee.Name,
                        Amount = fee.Amount
                    };
                });
            }
        }
    }
}