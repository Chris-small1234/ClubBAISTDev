using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAISTDev.Pages
{
    public class RequestStandingTeeTimeModel : PageModel
    {
        [BindProperty]
        public int MemberIdField { get; set; }
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

        public string Message { get; set; }

        CBS RequestDirector = new();


        public void OnGet()
        {
        }

        public void OnPost()
        {
            bool Confirmation;

            Confirmation = RequestDirector.CreateStandingTeeTimeRequest(MemberIdField, RequestedTeeTimeField, RequestedDayOfWeekField, RequestedStartDateField, RequestedEndDateField, false);

            if (Confirmation)
            {
                Message = "Standing Tee Time Request Submitted!";
            }
            else
            {
                Message = "Error submitting standing tee time request, please try again";
            }
        }
    }
}
