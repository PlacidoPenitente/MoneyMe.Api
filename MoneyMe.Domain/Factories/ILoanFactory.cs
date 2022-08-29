﻿using MoneyMe.Domain.ApplicationAggregate;
using MoneyMe.Domain.LoanAggregate;
using System;
using System.Collections.Generic;

namespace MoneyMe.Domain.Factories
{
    public interface ILoanFactory
    {
        Loan Create(
            Guid customerId,
            decimal loanAmount,
            int terms,
            decimal interestRate,
            IReadOnlyCollection<Term> monthlyAmortization);
    }
}