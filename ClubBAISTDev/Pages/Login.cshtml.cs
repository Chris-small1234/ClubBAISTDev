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

        [BindProperty]
        public int StaffMemberIdField { get; set; }

        [BindProperty]
        public string StaffPasswordField { get; set; }

        public string Message { get; set; }

        [BindProperty]
        public string Submit { get; set; }

        CBS RequestDirector = new();

        bool Success;

        public string logout;

        public void OnGet()
        {
            string user = HttpContext.Session.GetString("MemberAuth");
            if (user == null)
            {
                user = HttpContext.Session.GetString("StaffAuth");
            }
            logout = HttpContext.Session.GetString("Logout");
            if (user == null || user == "none")
            {
                HttpContext.Session.SetString("MemberAuth", "none");
                HttpContext.Session.SetString("StaffAuth", "none");
                HttpContext.Session.SetString("Logout", "false");
            } else
            {
                HttpContext.Session.SetString("Logout", "true");
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                switch(Submit)
                {
                    case "memberlogin":
                        Success = RequestDirector.Login(MemberIdField, PasswordField);

                        if (Success)
                        {
                            HttpContext.Session.SetString("MemberAuth", MemberIdField.ToString());
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
                    case "stafflogin":
                        Success = RequestDirector.StaffLogin(StaffMemberIdField, StaffPasswordField);

                        if (Success)
                        {
                            HttpContext.Session.SetString("StaffAuth", StaffMemberIdField.ToString());
                            HttpContext.Session.SetString("Logout", "true");
                            Message = "Staff Member Logged In!";
                            return new RedirectToPageResult("/Index");
                        }
                        else
                        {
                            Message = "Incorrect Staff Member ID or Password";
                            HttpContext.Session.SetString("Logout", "false");
                            return null;
                        }
                        break;
                    default:
                        HttpContext.Session.SetString("StaffAuth", "none");
                        HttpContext.Session.SetString("MemberAuth", "none");
                        HttpContext.Session.SetString("Logout", "false");
                        return new RedirectToPageResult("/Login");
                        break;
                }
            } catch
            {
                Message = "Something went wrong";
                return null;
            }

        }
    }
}
