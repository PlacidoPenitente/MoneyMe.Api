﻿using MoneyMe.Domain.LoanAggregate;
using MoneyMe.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MoneyMe.Domain.ApplicationAggregate
{
    public class Loan : IAggregate<Guid>
    {
        private readonly List<Term> _terms;

        private Loan()
        {

        }

        public Loan(
            Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            Guid customerId,
            decimal loanAmount,
            IReadOnlyCollection<Term> terms)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            CustomerId = customerId;
            LoanAmount = loanAmount;
            _terms = terms.ToList();
        }

        [Key]
        public Guid Id { get; private set; }
        public DateTime DateAdded { get; private set; }
        public DateTime DateModified { get; private set; }
        public Guid CustomerId { get; private set; }

        [Column(TypeName = "decimal(5, 4)")]
        public decimal LoanAmount { get; private set; }
        
        public IReadOnlyCollection<Term> Terms => _terms;
        public LoanStatus Status { get; private set; }

        public void Approve()
        {
            if (Status == LoanStatus.Pending)
            {
                Status = LoanStatus.Active;
            }
        }

        public void Decline()
        {
            if (Status == LoanStatus.Pending)
            {
                Status = LoanStatus.Declined;
            }
        }

        public decimal Pay(decimal amount)
        {
            if (Status != LoanStatus.Active)
            {
                return amount;
            }

            while (amount > 0)
            {
                var unpaidTerm = _terms.OrderBy(term => term.Period).FirstOrDefault(term => term.Principal > 0);

                if (unpaidTerm == null)
                {
                    Status = LoanStatus.Completed;
                    break;
                }

                var remaining = unpaidTerm.Principal - amount;
                amount = 0;

                if (remaining < 0)
                {
                    remaining = 0;
                    amount = decimal.Negate(remaining);
                }

                var paidTerm = new Term(unpaidTerm.Period, unpaidTerm.Interest, remaining);

                _terms.Add(paidTerm);
                _terms.Remove(unpaidTerm);

                DateModified = DateTime.UtcNow;
            }

            return amount;
        }
    }
}