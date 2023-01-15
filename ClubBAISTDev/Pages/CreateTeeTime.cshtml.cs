using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace ClubBAISTDev.Pages
{
    public class CreateTeeTimeModel : PageModel
    {
        [BindProperty]
        public DateTime TeeSheetDateField { get; set; }

        [BindProperty]
        public int NumberOfPlayersField { get; set; }

        [BindProperty]
        public string PhoneField { get; set; }

        [BindProperty]
        public int NumberOfCartsField { get; set; }

        [BindProperty]
        public DateTime TeeTimeField { get; set; }

        [BindProperty]
        public string EmployeeNameField { get; set; }

        [BindProperty]
        public int MemberIdField { get; set; }

        [BindProperty]
        public int DailyTeeSheetIdField { get; set; }

        public DailyTeeSheet TeeSheet { get; set; }

        public List<TeeTime> TodayTeeTimes { get; set; }

        public Member ListedMember { get; set; }

        [BindProperty]
        public string Submit { get; set; }

        public string Message { get; set; }

        public CBS RequestDirector = new();

        public bool DailyTeeSheetConfirmation = false;

        public void OnGet()
        {

        }

        public void OnPost()
        {
            string user = HttpContext.Session.GetString("Auth");
            if (user != null && user != "none")
            {
                bool Confirmation;
                switch (Submit)
                {
                    case "SearchTeeSheet":
                        TeeSheet = RequestDirector.GetDailyTeeSheet(TeeSheetDateField);
                        if (TeeSheet.TeeSheetDayOfWeek == null)
                        {
                            Confirmation = RequestDirector.CreateTeeSheet(TeeSheetDateField, TeeSheetDateField.DayOfWeek.ToString());
                            if (Confirmation)
                            {
                                TeeSheet = RequestDirector.GetDailyTeeSheet(TeeSheetDateField);
                                Confirmation = false;
                                Message = "There are no Tee Times scheduled for that day";
                            }
                        }
                        else
                        {
                            DailyTeeSheetConfirmation = true;
                            TodayTeeTimes = RequestDirector.GetTeeTimes(TeeSheet.DailyTeeSheetId);
                        }
                        break;

                    case "RequestTeeTime":
                        TeeSheet = RequestDirector.GetDailyTeeSheet(TeeSheetDateField);
                        int DailyTeeSheetId = TeeSheet.DailyTeeSheetId;
                        Confirmation = RequestDirector.CreateTeeTime(NumberOfPlayersField, PhoneField, NumberOfCartsField, TeeTimeField, EmployeeNameField, MemberIdField, DailyTeeSheetId);
                        if (Confirmation)
                        {
                            Message = "Tee Time has been booked";
                        }
                        else if (Message == null)
                        {
                            Message = "Error adding Tee Time";
                        }
                        break;
                }
            } else
            {
                Message = "User needs to login first";
            }
        }
    }
}
