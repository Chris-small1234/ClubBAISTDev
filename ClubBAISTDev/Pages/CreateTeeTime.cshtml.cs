using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAISTDev.Pages
{
    public class CreateTeeTimeModel : PageModel
    {
        [BindProperty]
        public DateTime TeeSheetDateField { get; set; }

        public DailyTeeSheet TeeSheet { get; set; }

        public List<TeeTime> TodayTeeTimes { get; set; }

        public Member ListedMember { get; set; }

        [BindProperty]
        public string Submit { get; set; }

        public string Message { get; set; }

        public CBS RequestDirector = new();

        public void OnGet()
        {

        }

        public void OnPost()
        {
            switch(Submit)
            {
                case "SearchTeeSheet":

                    TeeSheet = RequestDirector.GetDailyTeeSheet(TeeSheetDateField);
                    TodayTeeTimes = RequestDirector.GetTeeTimes(TeeSheet.DailyTeeSheetId);
                    //ListedMember = RequestDirector.GetMember();
                    break;

                case "RequestTeeTime":
                    break;
            }
        }
    }
}
