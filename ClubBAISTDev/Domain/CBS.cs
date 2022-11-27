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
    }
}
