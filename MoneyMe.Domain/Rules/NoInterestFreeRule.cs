﻿using Microsoft.VisualBasic;
using MoneyMe.Domain.LoanAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Rules
{
    public sealed class NoInterestFreeRule : IRule
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyCollection<Term> GenerateTerms(decimal loanAmount, int terms, decimal interestRate, decimal monthlyPayment)
        {
            var paymentTerms = new List<Term>();

            for (int period = 1; period < terms + 1; period++)
            {
                var interest = Convert.ToDecimal(Financial.IPmt(decimal.ToDouble(interestRate), period, terms, decimal.ToDouble(loanAmount)));
                paymentTerms.Add(new Term(period, interest, monthlyPayment - interest));
            }

            return paymentTerms;
        }
    }
}