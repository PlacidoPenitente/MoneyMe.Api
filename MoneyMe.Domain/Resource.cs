using System;

namespace MoneyMe.Domain
{
    public class Resource
    {
        public Guid Id { get; set; }
        public ResourceType Type { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
    }
}