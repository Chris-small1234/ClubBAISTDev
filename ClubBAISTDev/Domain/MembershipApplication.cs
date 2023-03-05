using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.Domain
{
    public class MembershipApplication
    {
        public int MembershipApplicationId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Occupation { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPostalCode { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public int MemberReference1 { get; set; }

        public int MemberReference2 { get; set; }

        public int ReviewedBy { get; set; }

        public bool Approved { get; set; }
    }
}
