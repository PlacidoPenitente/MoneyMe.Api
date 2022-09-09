using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyMe.Domain
{
    [Table("blacklist")]
    public class Blacklist : IAggregate<Guid>
    {
        private readonly List<string> _collection;

        public Blacklist(Guid id, DateTime dateAdded)
        {
            Id = id;
            DateCreated = dateAdded;
            DateModified = null;
            _collection = new List<string>();
        }

        [Key]
        public Guid Id { get; private set; }

        public DateTime DateCreated { get; private set; }

        public DateTime? DateModified { get; private set; }

        public IReadOnlyCollection<string> Collection => _collection;

        public void AddToBlacklist(string data)
        {
            _collection.Add(data);
        }

        public void RemoveFromBlacklist(string data)
        {
            _collection.Remove(data);
        }
    }
}