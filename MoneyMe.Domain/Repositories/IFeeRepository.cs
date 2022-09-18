using MoneyMe.Domain.FeeAggregate;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface IFeeRepository : IRepository<Fee>
    {
        Task<Fee> GetByNameAsync(string name);
    }
}