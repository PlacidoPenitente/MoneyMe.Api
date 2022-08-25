using System.Threading.Tasks;
using System;
using MoneyMe.Domain.ApplicationAggregate;

namespace MoneyMe.Domain.Repositories
{
    public interface ILoanRepository
    {
        Task AddAsync(Loan loan);
        Task<Loan> GetAsync(Guid id);
        Task<Loan> FindByCustomerIdAsync(Guid customerId);
        Task RemoveAsync(Guid id);
    }
}