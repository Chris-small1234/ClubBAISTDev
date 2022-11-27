using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAISTDev.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public int MemberIdField { get; set; }

        [BindProperty]
        public string PasswordField { get; set; }

        public string Message { get; set; }

        CBS RequestDirector = new();

        bool Success;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            Success = RequestDirector.Login(MemberIdField, PasswordField);

            if (Success)
            {
                Message = "Member Logged In! Redirecting...";
            } else
            {
                Message = "Incorrect Member ID or Password";
            }
        }
    }
}
