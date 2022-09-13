using System;

namespace MoneyMe.Application.Contracts.Dtos
{
    public class CustomerDto
    {
        public CustomerDto()
        {
            Id = Guid.Empty;
        }

        public CustomerDto(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}