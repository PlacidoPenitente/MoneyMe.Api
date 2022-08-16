using System;

namespace MoneyMe.Domain.ApplicationAggregate
{
    public class Application : IAggregate<Guid>
    {
        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public decimal FinanceAmount { get; }
        public int Repayments { get; }
        public Guid CustomerId { get; }
    }
}