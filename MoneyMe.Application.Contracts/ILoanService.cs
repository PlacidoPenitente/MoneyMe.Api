using System;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface ILoanService
    {
        Task AppyAsync(Guid quoteId);
    }
}