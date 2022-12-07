using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.TechnicalServices;

namespace ClubBAISTDev.Domain
{
    public class CBS
    {
        public bool Login(int memberId, string memberPassword)
        {
            bool Success = false;
            Members MemberManager = new();
            Success = MemberManager.Login(memberId, memberPassword);

            return Success;
        }

        public DailyTeeSheet GetDailyTeeSheet(DateTime selectedDate)
        {
            DailyTeeSheets DailyTeeSheetManager = new();
            DailyTeeSheet TeeSheet = new();
            TeeSheet = DailyTeeSheetManager.GetDailyTeeSheet(selectedDate);

            return TeeSheet;
        }

        public List<TeeTime> GetTeeTimes(int dailyTeeSheetId)
        {
            TeeTimes TeeTimeManager = new();
            List<TeeTime> TodayTeeTimes = new();
            TodayTeeTimes = TeeTimeManager.GetTeeTimes(dailyTeeSheetId);

            return TodayTeeTimes;
        }

        public bool CreateTeeTime(DateTime TimeSlotField, int NumberOfPlayersField, string PhoneField, int NumberOfCartsField, DateTime TeeTimeDateField, DateTime SetTeeTimeField, string EmployeeNameField, int MemberIdField, int DailyTeeSheetIdField)
        {
            TeeTime RequestedTeeTime = new()
            {
                TimeSlot = TimeSlotField,
                NumberOfPlayers = NumberOfPlayersField,
                Phone = PhoneField,
                NumberOfCarts = NumberOfCartsField,
                TeeDate = TeeTimeDateField,
                SetTeeTime = SetTeeTimeField,
                EmployeeName = EmployeeNameField,
                MemberId = MemberIdField,
                DailyTeeSheetId = DailyTeeSheetIdField
            };
            TeeTimes TeeTimeManager = new();
            bool Confirmation;
            Confirmation = TeeTimeManager.CreateTeeTime(RequestedTeeTime);

            return Confirmation;
        }
    }
}
