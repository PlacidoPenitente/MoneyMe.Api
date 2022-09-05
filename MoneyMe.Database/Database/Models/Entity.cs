using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyMe.Infrastructure.Database.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }
    }
}