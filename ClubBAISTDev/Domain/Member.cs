using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.Domain
{
    public class Member
    {
        public int MemberId { get; set; }

        public string MembershipLevel { get; set; }

        public string MembershipType { get; set; }

        public string MemberName { get; set; }

        public string MemberStanding { get; set; }

        public string MemberEmail { get; set; }
    }
}
