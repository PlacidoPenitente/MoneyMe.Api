using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Infrastructure.Database.Models
{
    [Table("customers")]
    public class Customer : Entity
    {
        public string Title { get; set; }
         
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string MobileNumber { get; set; }
        
        public string EmailAddress { get; set; }
    }
}