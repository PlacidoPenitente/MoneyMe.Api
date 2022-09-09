using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface IFeeService
    {
        Task<FeeDto> CreateFeeAsync(FeeDto feeDto);
        Task<FeeDto> ReadFeeAsync(Guid id);
        Task<IEnumerable<FeeDto>> ReadAllFeesAsync();
        Task<FeeDto> UpdateFeeAsync(FeeDto feeDto);
        Task DeleteFeeAsync(Guid id);
    }
}