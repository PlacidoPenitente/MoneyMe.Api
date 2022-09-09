using System;

namespace MoneyMe.Api.Models
{
    public class Fee
    {
        public Fee(Guid id, DateTime dateAdded, DateTime dateModified)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
    }
}