using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Infrastructure.Database.Models
{
    [Table("rules")]
    public class Rule : Entity
    {
        public string Name { get; set; }
    }
}