using System.Threading.Tasks;
using System;

namespace MoneyMe.Domain
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> task);
        Task ExecuteAsync(Func<Task> task);
        Task<int> CommitAsync();
    }
}