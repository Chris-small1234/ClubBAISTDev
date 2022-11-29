using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.Domain
{
    public class TeeTime
    {
        public int TeeTimeId { get; set; }

        public DateTime TimeSlot { get; set; }

        public int NumberOfPlayers { get; set; }

        public string Phone { get; set; }

        public int NumberOfCarts { get; set; }

        public DateTime TeeDate { get; set; }

        public DateTime SetTeeTime { get; set; }

        public string EmployeeName { get; set; }

        public int MemberId { get; set; }

        public int DailyTeeSheetId { get; set; }
    }
}
