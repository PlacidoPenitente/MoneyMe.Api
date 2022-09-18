using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Infrastructure.Database.Models
{
    [Table("product_fees")]
    public class ProductFee
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid FeeId { get; set; }
        public Fee Fee { get; set; }
    }
}