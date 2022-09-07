using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Infrastructure.Database.Models
{
    [Table("loans")]
    public class Loan : Entity
    {
        public Guid CustomerId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal LoanAmount { get; set; }

        public ICollection<Payment> MonthlyAmortization { get; set; }
    }
}