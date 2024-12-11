using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SSN { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? StreetAddress { get; set; }
        public int? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public Customer(int customerId, string firstName, string lastName,
            int ssn, int phoneNumber, string email, string? street, int? postalCode, string? city, string? country)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            SSN = ssn;
            PhoneNumber = phoneNumber;
            Email = email;
            StreetAddress = street;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }
    }
}
