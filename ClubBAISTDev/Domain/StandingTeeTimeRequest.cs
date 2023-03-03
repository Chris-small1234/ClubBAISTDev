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

        public string Player1Name { get; set; }

        public string Player2Name { get; set; }

        public string Player3Name { get; set; }

        public string Player4Name { get; set; }

    }
}
