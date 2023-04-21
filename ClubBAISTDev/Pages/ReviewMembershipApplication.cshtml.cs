using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.Domain;
using ClubBAISTDev.TechnicalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAISTDev.Pages
{
    public class ReviewMembershipApplicationModel : PageModel
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
        public string EmailField { get; set; }

        [BindProperty]
        public bool ApprovedField { get; set; }

        [BindProperty]
        public int MembershipApplicationField { get; set; }

        [BindProperty]
        public string MembershipLevelField { get; set; }

        [BindProperty]
        public string MembershipTypeField { get; set; }

        [BindProperty]
        public string Submit { get; set; }

        public string Message { get; set; }

        public List<MembershipApplication> AllMembershipApplications { get; set; }

        public MembershipApplication CurrentMembershipApplication { get; set; }

        public Member Reference1 { get; set; }

        public Member Reference2 { get; set; }

        public string[] MembershipLevels = { "G", "S", "B", "C" };

        CBS RequestDirector = new();


        public void OnGet()
        {
            string user = HttpContext.Session.GetString("StaffAuth");
            if (user != null && user != "none")
            {
                StaffMember CurrentStaffMember = RequestDirector.GetStaffMember(int.Parse(user));
                if (CurrentStaffMember.StaffTypeName == "Membership Committee")
                    AllMembershipApplications = RequestDirector.GetMembershipApplications();
                else
                    Message = "Only the Membership Committee can review a membership application";
            }
            else
                Message = "User needs to login first";
        }

        public void OnPost()
        {
            AllMembershipApplications = RequestDirector.GetMembershipApplications();
            string user = HttpContext.Session.GetString("StaffAuth");

            if (user != null && user != "none")
            {
                StaffMember CurrentStaffMember = RequestDirector.GetStaffMember(int.Parse(user));

                if (CurrentStaffMember.StaffTypeName == "Membership Committee")
                {
                    if (Submit == "GetMembershipApplication")
                    {
                        CurrentMembershipApplication = RequestDirector.GetMembershipApplication(MembershipApplicationField);
                        Reference1 = RequestDirector.GetMember(CurrentMembershipApplication.MemberReference1);
                        Reference2 = RequestDirector.GetMember(CurrentMembershipApplication.MemberReference2);
                    } else
                    {
                        if (MembershipLevelField != null)
                        {
                            Member NewMember = new()
                            {
                                MembershipLevel = MembershipLevelField,
                                MembershipType = MembershipTypeField,
                                MemberName = FirstNameField + " " + LastNameField,
                                MemberEmail = EmailField,
                                MemberStanding = "G",
                                Balance = 0
                            };
                            bool Confirmation = RequestDirector.CreateMember(NewMember);

                            if (Confirmation)
                            {
                                Message = "Member Created!";
                            }
                            else
                            {
                                Message = "Error creating member, please try again";
                            }
                        } else
                        {
                            Message = "Membership Level Required";
                        }

                    }
                }
                else
                {
                    Message = "Only the Membership Committee can review a membership application";
                }
            }
            else
            {
                Message = "User needs to login first";
            }
        }
    }
}
