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

        [BindProperty]
        public DateTime TimeSlotField { get; set; }

        [BindProperty]
        public int NumberOfPlayersField { get; set; }

        [BindProperty]
        public string PhoneField { get; set; }

        [BindProperty]
        public int NumberOfCartsField { get; set; }

        [BindProperty]
        public DateTime SetTeeTimeField { get; set; }

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
            bool Confirmation;
            switch(Submit)
            {
                case "SearchTeeSheet":
                    TeeSheet = RequestDirector.GetDailyTeeSheet(TeeSheetDateField);
                    if (TeeSheet.TeeSheetDayOfWeek == null)
                    {
                        Message = "There are no Tee Times on the selected day";

                    } else
                    {
                        DailyTeeSheetConfirmation = true;
                        TodayTeeTimes = RequestDirector.GetTeeTimes(TeeSheet.DailyTeeSheetId);
                    }
                    break;

                case "RequestTeeTime":
                    /*
                    foreach (var teeTime in TodayTeeTimes)
                    {
                        TimeSpan requestedTeeTimeSpan = RequestedTeeTime.SetTeeTime - new DateTime(1970, 1, 1);
                        int requestedTeeTimeSecondsSinceEpoch = (int)requestedTeeTimeSpan.TotalSeconds;

                        TimeSpan existingTeeTimeSpan = teeTime.SetTeeTime - new DateTime(1970, 1, 1);
                        int existingTeeTimeSecondsSinceEpoch = (int)existingTeeTimeSpan.TotalSeconds;

                        int eightMinutesInSeconds = 480000;

                        if (requestedTeeTimeSecondsSinceEpoch - eightMinutesInSeconds != existingTeeTimeSecondsSinceEpoch || requestedTeeTimeSecondsSinceEpoch + eightMinutesInSeconds != existingTeeTimeSecondsSinceEpoch)
                        {
                            Confirmation = RequestDirector.CreateTeeTime(RequestedTeeTime);
                        } else
                        {
                            Confirmation = false;
                            Message = "Cannot book tee time, another member has already book at that time";
                        }
                    }
                    */
                    DateTime TeeTimeDateField = SetTeeTimeField.Date;
                    TeeSheet = RequestDirector.GetDailyTeeSheet(SetTeeTimeField);
                    int DailyTeeSheetId = TeeSheet.DailyTeeSheetId;
                    Confirmation = RequestDirector.CreateTeeTime(TimeSlotField, NumberOfPlayersField, PhoneField, NumberOfCartsField, TeeTimeDateField, SetTeeTimeField, EmployeeNameField, MemberIdField, DailyTeeSheetId);
                    if (Confirmation)
                    {
                        Message = "Tee Time has been booked";
                    } else if (Message == null)
                    {
                        Message = "Error adding Tee Time";
                    }
                    break;
            }
        }
    }
}
