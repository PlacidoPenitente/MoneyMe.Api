using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Infrastructure.Database.Models
{
    [Table("products")]
    public class Product : Entity
    {
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal InterestRate { get; set; }

        public int MaximumDuration { get; set; }

        public int MinimumDuration { get; set; }

        public string Rule { get; set; }
    }
}