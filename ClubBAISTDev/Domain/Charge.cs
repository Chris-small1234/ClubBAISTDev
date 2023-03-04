using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.Domain
{
    public class Charge
    {
        public int ChargeId { get; set; }

        public int MemberId { get; set; }

        public decimal Amount { get; set; }

        public DateTime WhenCharged { get; set; }

        public DateTime WhenBooked { get; set; }
    }
}
