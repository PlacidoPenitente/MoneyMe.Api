using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Infrastructure.Database.Models
{
    [Table("fees")]
    public class Fee : Entity
    {
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}