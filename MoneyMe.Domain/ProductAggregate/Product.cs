using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.ProductAggregate
{
    public class Product : IAggregate<Guid>
    {
        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public string Name { get; }
        public IReadOnlyCollection<Term> Terms { get; }
    }
}