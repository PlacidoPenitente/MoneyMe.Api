using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.Shared;
using System.Collections.Generic;
using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class LoanDto
    {
        public Guid Id { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public Guid CustomerId { get; }
        public decimal LoanAmount { get; }
        public IReadOnlyCollection<TermDto> Terms { get; set; }
        public LoanStatus Status { get; set; }
    }
}