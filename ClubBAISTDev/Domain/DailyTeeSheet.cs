using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.Domain
{
    public class DailyTeeSheet
    {
        public int DailyTeeSheetId { get; set; }

        public DateTime TeeSheetDate { get; set; }

        public string TeeSheetDayOfWeek { get; set; }
    }
}
