using System;
using MoneyMe.Domain.CustomerAggregate;
using MoneyMe.Domain.Shared;

namespace MoneyMe.Domain.Factories
{
    public class CustomerFactory : ICustomerFactory
    {
        public Customer Create(
            string title,
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            string mobileNumber,
            string emailAddress)
        {
            return new Customer(
                Guid.NewGuid(),
                DateTime.UtcNow,
                null,
                Enum.Parse<Title>(title.Replace(".", string.Empty)),
                firstName,
                lastName,
                dateOfBirth,
                mobileNumber,
                emailAddress);
        }
    }
}