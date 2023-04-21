using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAISTDev.Pages
{
    public class CreateMembershipApplicationModel : PageModel
    {
        [BindProperty]
        public string FirstNameField { get; set; }

        [BindProperty]
        public string LastNameField { get; set; }

        [BindProperty]
        public string AddressField { get; set; }

        [BindProperty]
        public string PostalCodeField { get; set; }

        [BindProperty]
        public string PhoneNumberField { get; set; }

        [BindProperty]
        public string AlternatePhoneNumberField { get; set; }

        [BindProperty]
        public string EmailField { get; set; }

        [BindProperty]
        public DateTime DateOfBirthField { get; set; }

        [BindProperty]
        public string OccupationField { get; set; }

        [BindProperty]
        public string CompanyNameField { get; set; }

        [BindProperty]
        public string CompanyAddressField { get; set; }

        [BindProperty]
        public string CompanyPostalCodeField { get; set; }

        [BindProperty]
        public string CompanyPhoneNumberField { get; set; }

        [BindProperty]
        public int Reference1Field { get; set; }

        [BindProperty]
        public int Reference2Field { get; set; }

        public string Message { get; set; }

        CBS RequestDirector = new();

        public List<Member> Members { get; set; }

        public void OnGet()
        {
            Members = RequestDirector.GetMembers();
        }

        public void OnPost()
        {
            Members = RequestDirector.GetMembers();
            string user = HttpContext.Session.GetString("StaffAuth");

            if (user != null && user != "none")
            {
                StaffMember CurrentStaffMember = RequestDirector.GetStaffMember(int.Parse(user));
                if (CurrentStaffMember.StaffTypeName == "Membership Committee")
                {
                    if (FirstNameField == null)
                    {
                        ModelState.AddModelError("FirstNameField", "First Name is not valid");
                    }
                    if (LastNameField == null)
                    {
                        ModelState.AddModelError("LastNameField", "Last Name is not valid");
                    }
                    if (AddressField == null)
                    {
                        ModelState.AddModelError("AddressField", "Address is not valid");
                    }
                    if (PostalCodeField == null)
                    {
                        ModelState.AddModelError("PostalCodeField", "Postal Code is not valid");
                    }
                    if (PhoneNumberField == null)
                    {
                        ModelState.AddModelError("PhoneNumberField", "Phone Number is not valid");
                    }
                    if (EmailField == null)
                    {
                        ModelState.AddModelError("EmailField", "Email is not valid");
                    }
                    if (Reference1Field == 0)
                    {
                        ModelState.AddModelError("Reference1Field", "Reference 1 is required");
                    }
                    if (Reference2Field == 0)
                    {
                        ModelState.AddModelError("Reference2Field", "Reference 2 is required");
                    }

                    if (ModelState.ErrorCount == 0)
                    {
                        MembershipApplication NewMembershipApplication = new()
                        {
                            FirstName = FirstNameField,
                            LastName = LastNameField,
                            Address = AddressField,
                            PostalCode = PostalCodeField,
                            PhoneNumber = PhoneNumberField,
                            AlternatePhoneNumber = AlternatePhoneNumberField,
                            Email = EmailField,
                            DateOfBirth = DateOfBirthField,
                            Occupation = OccupationField,
                            CompanyName = CompanyNameField,
                            CompanyAddress = CompanyAddressField,
                            CompanyPostalCode = CompanyPostalCodeField,
                            CompanyPhoneNumber = CompanyPhoneNumberField,
                            MemberReference1 = Reference1Field,
                            MemberReference2 = Reference2Field,
                            Approved = false,
                        };

                        bool Confirmation = RequestDirector.CreateMembershipApplication(NewMembershipApplication);

                        if (Confirmation)
                        {
                            Message = "Membership Application Submitted!";
                        }
                        else
                        {
                            Message = "Error membership application, please try again";
                        }
                    }
                } else
                {
                    Message = "Only the Membership Committee can create a membership application";
                }
            } else
            {
                Message = "User needs to login first";
            }
        }
    }
}
