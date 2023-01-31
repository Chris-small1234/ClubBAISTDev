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

        [BindProperty]
        public string Submit { get; set; }

        CBS RequestDirector = new();

        bool Success;

        public string logout;

        public void OnGet()
        {
            string user = HttpContext.Session.GetString("Auth");
            logout = HttpContext.Session.GetString("Logout");
            if (user == null || user == "none")
            {
                HttpContext.Session.SetString("Auth", "none");
                HttpContext.Session.SetString("Logout", "false");
            } else
            {
                HttpContext.Session.SetString("Logout", "true");
            }
        }

        public IActionResult OnPost()
        {
            switch(Submit)
            {
                case "login":
                    Success = RequestDirector.Login(MemberIdField, PasswordField);

                    if (Success)
                    {
                        HttpContext.Session.SetString("Auth", MemberIdField.ToString());
                        HttpContext.Session.SetString("Logout", "true");
                        Message = "Member Logged In!";
                        return new RedirectToPageResult("/Index");
                    }
                    else
                    {
                        Message = "Incorrect Member ID or Password";
                        HttpContext.Session.SetString("Logout", "false");
                        return null;
                    }
                    break;
                case "logout":
                    HttpContext.Session.SetString("Auth", "none");
                    HttpContext.Session.SetString("Logout", "false");
                    return new RedirectToPageResult("/Login");
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}
