using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

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
            string user = HttpContext.Session.GetString("Auth");
            if (user == null)
            {
                HttpContext.Session.SetString("Auth", "none");
            } else
            {
                Message = "User already logged in!";
            }
        }

        public IActionResult OnPost()
        {
            string user = HttpContext.Session.GetString("Auth");
            if (user == null || user == "none")
            {
                Success = RequestDirector.Login(MemberIdField, PasswordField);

                if (Success)
                {
                    HttpContext.Session.SetString("Auth", MemberIdField.ToString());
                    Message = "Member Logged In!";
                    return new RedirectToPageResult("/Index");
                } else
                {
                    Message = "Incorrect Member ID or Password";
                    return null;
                }
            } else
            {
                Message = "User already logged in!";
                return null;
            }
        }
    }
}
