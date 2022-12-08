using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public void OnGet()
        {
        }
    }
}
