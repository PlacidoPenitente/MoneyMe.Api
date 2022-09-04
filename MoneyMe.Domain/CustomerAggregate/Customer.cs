using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NodaTime;

namespace MoneyMe.Domain.CustomerAggregate
{
    public class Customer : IAggregate<Guid>
    {
        private readonly IReadOnlyCollection<string> _titles = new List<string>() { "Mr.", "Ms.", "Mrs." };

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

        public void ChangeTitle(string title)
        {
            title = title?.Trim();

            if (_titles.Any(x => x == title))
            {
                LastName = title;
                DateModified = DateTime.UtcNow;
            }

            throw new ArgumentException("Invalid title.");
        }

        public void ChangeFirstName(string firstName)
        {
            firstName = firstName?.Trim();

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("Invalid first name.");
            }

            FirstName = firstName;
            DateModified = DateTime.UtcNow;
        }

        public void ChangeLastName(string lastName)
        {
            lastName = lastName?.Trim();

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Invalid last name.");
            }

            LastName = lastName.Trim();
            DateModified = DateTime.UtcNow;
        }

        public void ChangeDateOfBirth(DateTime dateOfBirth)
        {
            var currentDate = LocalDate.FromDateTime(DateTime.UtcNow);
            var newDateOfBirth = LocalDate.FromDateTime(dateOfBirth);
            var age = Period.Between(newDateOfBirth, currentDate).Years;

            if (age < 18 || age > 75)
            {
                throw new ArgumentException("Age must be between 18 years old and 75 years old.");
            }

            DateOfBirth = currentDate.ToDateTimeUnspecified();
            DateModified = DateTime.UtcNow;
        }

        public void ChangeMobileNumber(string mobileNumber)
        {
            mobileNumber = mobileNumber?.Trim();

            if (string.IsNullOrWhiteSpace(mobileNumber))
            {
                throw new ArgumentException("Invalid mobile number.");
            }

            MobileNumber = mobileNumber;
            DateModified = DateTime.UtcNow;
        }

        public void ChangeEmailAddress(string emailAddress)
        {
            emailAddress = emailAddress?.Trim();

            if (string.IsNullOrWhiteSpace(emailAddress.Trim()))
            {
                throw new ArgumentException("Invalid email address.");
            }

            EmailAddress = emailAddress;
            DateModified = DateTime.UtcNow;
        }
    }
}