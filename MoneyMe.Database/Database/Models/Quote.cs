using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Infrastructure.Database.Models
{
    [Table("quotes")]
    public class Quote : Entity
    {
        public Guid CustomerId { get; set; }

        public Guid ProductId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal LoanAmount { get; set; }

        public int Term { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Interest { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Fee { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MonthlyPayment { get; set; }
    }
}