using System;

namespace Phonebook.Models
{
    public class Contact
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
    }
}