using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Infrastructure.Database.Models
{
    [Table("payments")]
    public sealed class Payment : Entity
    {
        public int Period { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Interest { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Principal { get; set; }
    }
}