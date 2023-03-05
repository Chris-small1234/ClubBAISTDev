using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAISTDev.Pages
{
    public class ViewMemberAccountModel : PageModel
    {
        CBS RequestDirector = new();

        public Member LoggedInMember;

        public List<Charge> AllCharges;

        public string Message { get; set; }

        public string user { get; set; }

        public void OnGet()
        {
            user = HttpContext.Session.GetString("MemberAuth");
            if (user != null && user != "none")
            {
                LoggedInMember = RequestDirector.GetMember(int.Parse(user));
                AllCharges = RequestDirector.GetChargesByMember(int.Parse(user));

                switch(LoggedInMember.MembershipLevel)
                {
                    case "G":
                        LoggedInMember.MembershipLevel = "Gold";
                        break;
                    case "S":
                        LoggedInMember.MembershipLevel = "Silver";
                        break;

                    case "B":
                        LoggedInMember.MembershipLevel = "Bronze";
                        break;

                    case "C":
                        LoggedInMember.MembershipLevel = "Copper";
                        break;
                }

                switch (LoggedInMember.MemberStanding)
                {
                    case "G":
                        LoggedInMember.MemberStanding = "Good";
                        break;
                    default:
                        LoggedInMember.MemberStanding = "Bad";
                        break;
                }

                foreach(Charge element in AllCharges)
                {
                    LoggedInMember.Balance += element.Amount;
                }
            }
            else
                Message = "Member needs to be logged in";
        }
    }
}
