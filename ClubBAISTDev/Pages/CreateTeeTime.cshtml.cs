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
        public int Player1ListEntry { get; set; }

        [BindProperty]
        public int Player2ListEntry { get; set; }

        [BindProperty]
        public int Player3ListEntry { get; set; }

        [BindProperty]
        public int Player4ListEntry { get; set; }

        public DailyTeeSheet TeeSheet { get; set; }

        public List<TeeTime> TodayTeeTimes { get; set; }

        public Member ListedMember { get; set; }

        public List<Member> Members { get; set; }

        [BindProperty]
        public string Submit { get; set; }

        public string Message { get; set; }

        public CBS RequestDirector = new();

        public bool DailyTeeSheetConfirmation = false;

        public void OnGet()
        {
            Members = RequestDirector.GetMembers();
        }

        public void OnPost()
        {
            Members = RequestDirector.GetMembers();
            string user = HttpContext.Session.GetString("MemberAuth");
            if (user != null && user != "none")
            {
                bool Confirmation;
                ReturnItem Return = new();
                switch (Submit)
                {
                    case "SearchTeeSheet":
                        Members = RequestDirector.GetMembers();
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
                            int[] PlayingMemberIds = { Player1ListEntry, Player2ListEntry, Player3ListEntry, Player4ListEntry };
                            List<Player> Players = new();
                            for(int i = 0; i < PlayingMemberIds.Length; i++)
                            {
                                if (PlayingMemberIds[i] > 0)
                                {
                                    Member CurrentMember = RequestDirector.GetMember(PlayingMemberIds[i]);
                                    Player NewPlayer = new()
                                    {
                                        MemberId = CurrentMember.MemberId,
                                        PlayerName = CurrentMember.MemberName,
                                    };
                                    Players.Add(NewPlayer);
                                }
                            }
                            Return = RequestDirector.CreateTeeTime(PhoneField, NumberOfCartsField, TeeTimeField, EmployeeNameField, MemberId, DailyTeeSheetId, Players);
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
