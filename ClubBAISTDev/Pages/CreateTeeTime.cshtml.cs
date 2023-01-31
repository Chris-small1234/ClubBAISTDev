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

        [BindProperty]
        public string  Player1NameField { get; set; }

        [BindProperty]
        public int Player1IdField { get; set; }

        [BindProperty]
        public string Player2NameField { get; set; }

        [BindProperty]
        public int Player2IdField { get; set; }

        [BindProperty]
        public string Player3NameField { get; set; }

        [BindProperty]
        public int Player3IdField { get; set; }

        [BindProperty]
        public string Player4NameField { get; set; }

        [BindProperty]
        public int Player4IdField { get; set; }

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
                ReturnItem Return = new();
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
                        int[] times = { 00, 07, 15, 22, 30, 37, 45, 52 };
                        TeeSheet = RequestDirector.GetDailyTeeSheet(TeeSheetDateField);
                        int DailyTeeSheetId = TeeSheet.DailyTeeSheetId;
                        int MemberId = int.Parse(user);
                        bool correct = false;
                        foreach(int number in times) {
                            if (number == TeeTimeField.TimeOfDay.Minutes)
                            {
                                correct = true;
                            }
                        }
                        if (correct)
                        {
                            Player Player1 = new()
                            {
                                MemberId = Player1IdField,
                                PlayerName = Player1NameField
                            };
                            Player Player2 = new()
                            {
                                MemberId = Player2IdField,
                                PlayerName = Player2NameField
                            };
                            Player Player3 = new()
                            {
                                MemberId = Player3IdField,
                                PlayerName = Player3NameField
                            };
                            Player Player4 = new()
                            {
                                MemberId = Player4IdField,
                                PlayerName = Player4NameField
                            };
                            Player[] PlayingPlayers = { Player1, Player2, Player2, Player3, Player4 };
                            Return = RequestDirector.CreateTeeTime(NumberOfPlayersField, PhoneField, NumberOfCartsField, TeeTimeField, EmployeeNameField, MemberId, DailyTeeSheetId, PlayingPlayers);
                            if (Return.Result)
                            {
                                Message = Return.Message;
                            }
                            else if (Message == null)
                            {
                                Message = Return.Message;
                            }
                        } else
                        {
                            Message = "Tee Time must be in an increment of 7 or 8 minutes";
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
