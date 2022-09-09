using System;

namespace MoneyMe.Domain
{
    public interface IAggregate<T>
    {
        public T Id { get; }
        public DateTime DateCreated { get; }
        public DateTime? DateModified { get; }
    }
}