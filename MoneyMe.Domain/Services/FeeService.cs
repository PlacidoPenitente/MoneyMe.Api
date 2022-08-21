using MoneyMe.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Services
{
    public class FeeService
    {
        private readonly IFeeRepository _feeRepository;

        public FeeService(IFeeRepository feeRepository)
        {
            _feeRepository = feeRepository;
        }

        public async Task<decimal> CalcualteTotalFees(IReadOnlyCollection<Guid> feeIds)
        {
            var totalFees = 0m;

            foreach (var feeId in feeIds)
            {
                var fee = await _feeRepository.GetAsync(feeId);
                totalFees += fee.Amount;
            }

            return totalFees;
        }
    }
}