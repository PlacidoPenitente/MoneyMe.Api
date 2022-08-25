using MoneyMe.Application.Contracts.Dtos;
using System;
using System.Threading.Tasks;

namespace MoneyMe.Application.Contracts
{
    public interface ILoanService
    {
        Task<LoanDto> ApplyAsync(Guid quoteId);
    }
}