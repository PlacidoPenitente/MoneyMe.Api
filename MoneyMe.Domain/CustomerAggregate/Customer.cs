using System;
using System.ComponentModel.DataAnnotations;

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

        [Key]
        public Guid Id { get; private set; }
        public DateTime DateAdded { get; private set; }
        public DateTime DateModified { get; private set; }
        public string Title { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string MobileNumber { get; private set; }
        public string EmailAddress { get; private set; }
    }
}