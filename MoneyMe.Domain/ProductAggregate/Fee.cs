using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Domain.ProductAggregate
{
    public class Fee
    {
        public Fee(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }

        public string Name { get; private set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; private set; }
    }
}