using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.Domain
{
    public class StandingTeeTimeRequest
    {
        public int MemberId { get; set; }

        public DateTime RequestedTeeTime { get; set; }

        public string DayOfWeek { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Approved { get; set; }
    }
}
