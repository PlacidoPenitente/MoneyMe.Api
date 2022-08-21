using MoneyMe.Domain.FeeAggregate;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface IFeeRepository
    {
        Task<Fee> GetAsync(Guid id);
    }
}