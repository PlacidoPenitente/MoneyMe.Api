using System;

namespace MoneyMe.Domain.CustomerAggregate
{
    public class Customer : IAggregate<Guid>
    {
        private Customer()
        {

        }

        public Customer(
            Guid id,
            DateTime dateAdded,
            DateTime dateModified,
            string title,
            string firstName,
            string lastName,
            DateTime dateOfBirth,
            string mobileNumber,
            string emailAddress)
        {
            Id = id;
            DateAdded = dateAdded;
            DateModified = dateModified;
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; }
        public DateTime DateModified { get; }
        public string Title { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }
        public string MobileNumber { get; }
        public string EmailAddress { get; }
    }
}