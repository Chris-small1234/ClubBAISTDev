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
    public class RequestStandingTeeTimeModel : PageModel
    {
        [BindProperty]
        public string MemberNameField { get; set; }
        [BindProperty]
        public string RequestedDayOfWeekField { get; set; }
        [BindProperty]
        public DateTime RequestedTeeTimeField { get; set; }
        [BindProperty]
        public DateTime RequestedStartDateField { get; set; }
        [BindProperty]
        public DateTime RequestedEndDateField { get; set; }
        [BindProperty]
        public string Player2NameField { get; set; }
        [BindProperty]
        public string Player3NameField { get; set; }
        [BindProperty]
        public string Player4NameField { get; set; }

        public List<Member> Members { get; set; }

        public string Message { get; set; }

        CBS RequestDirector = new();


        public void OnGet()
        {
            Members = RequestDirector.GetMembers();
        }

        public void OnPost()
        {
            Members = RequestDirector.GetMembers();
            bool Confirmation;
            string user = HttpContext.Session.GetString("MemberAuth");

            if (user != null && user != "none")
            {
                Member LoggedInMember = RequestDirector.GetMember(int.Parse(user));
                if (LoggedInMember.MembershipType == "Stakeholder")
                {
                    if (Player2NameField != null && Player3NameField != null && Player4NameField != null)
                    {
                        string Player1Name = LoggedInMember.MemberName;
                        Confirmation = RequestDirector.CreateStandingTeeTimeRequest(LoggedInMember.MemberId, RequestedTeeTimeField, RequestedDayOfWeekField, RequestedStartDateField, RequestedEndDateField, false, Player1Name, Player2NameField, Player3NameField, Player4NameField);

                        if (Confirmation)
                        {
                            Message = "Standing Tee Time Request Submitted!";
                        }
                        else
                        {
                            Message = "Error submitting standing tee time request, please try again";
                        }
                    }
                    else
                    {
                        Message = "There must be 4 members on a standing tee time request";
                    }
                } else
                {
                    Message = "Member needs to be a stakeholder to make a standing tee time request";
                }
            } else
            {
                Message = "User needs to login first";
            }
        }
    }
}
