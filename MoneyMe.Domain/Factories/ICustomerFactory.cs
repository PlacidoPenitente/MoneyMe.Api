using System;
using MoneyMe.Domain.CustomerAggregate;

namespace MoneyMe.Domain.Factories
{
    public interface ICustomerFactory
    {
        Customer Create(
            string title,
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            string mobileNumber,
            string emailAddress);
    }
}