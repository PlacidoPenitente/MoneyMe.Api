using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoneyMe.Infrastructure.Database.Models
{
    [Table("fees")]
    public class Fee : Entity
    {
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public bool IsPercentage { get; set; }

        public ICollection<ProductFee> ProductFees { get; set; }
    }
}