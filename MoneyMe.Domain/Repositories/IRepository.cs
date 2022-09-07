using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyMe.Domain.Repositories
{
    public interface IRepository<T> where T : class, IAggregate<Guid>
    {
        Task AddAsync(T t);
        Task<T> GetAsync(Guid id);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        void Update(T t);
        void Remove(T t);
    }
}