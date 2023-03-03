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
    public class CancelStandingTeeTimeRequestModel : PageModel
    {
        [BindProperty]
        public string StandingTeeTimeMemberIdField { get; set; }

        [BindProperty]
        public string Submit { get; set; }

        public List<StandingTeeTimeRequest> StandingTeeTimeRequests { get; set; }

        public string Message { get; set; }

        CBS RequestDirector = new();

        public StandingTeeTimeRequest StandingTeeTimeRequestField;


        public void OnGet()
        {
            StandingTeeTimeRequests = RequestDirector.GetStandingTeeTimeRequests();
        }

        public void OnPost()
        {
            StandingTeeTimeRequests = RequestDirector.GetStandingTeeTimeRequests();
            bool Confirmation;
            string user = HttpContext.Session.GetString("Auth");

            if (user != null && user != "none")
            {
                Member LoggedInMember = RequestDirector.GetMember(int.Parse(user));
                if (Submit == "CancelStandingTeeTime")
                {
                    // Cancel Request
                    Confirmation = RequestDirector.CancelStandingTeeTimeRequest(LoggedInMember.MemberId);

                    if (Confirmation)
                    {
                        Message = "Standing Tee Time Request Canceled!";
                    }
                    else
                    {
                        Message = "Error cancelling standing tee time request, please try again";
                    }
                } else
                {
                    StandingTeeTimeRequestField = RequestDirector.GetStandingTeeTimeRequestByMemberId(LoggedInMember.MemberId);
                }
            }
            else
            {
                Message = "User needs to login first";
            }
        }
    }
}
