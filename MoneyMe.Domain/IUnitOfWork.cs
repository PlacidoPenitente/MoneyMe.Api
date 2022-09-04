using System.Threading.Tasks;
using System;

namespace MoneyMe.Domain
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        Task<T> ExecuteAsync<T>(Func<Task<T>> task);
        Task ExecuteAsync(Func<Task> task);
        Task<int> CommitAsync();
    }
}