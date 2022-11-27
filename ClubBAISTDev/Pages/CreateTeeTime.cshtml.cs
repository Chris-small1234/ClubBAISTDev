using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubBAISTDev.Pages
{
    public class CreateTeeTimeModel : PageModel
    {
        [BindProperty]
        public DateTime TeeSheetDateField { get; set; }

        public void OnGet()
        {

        }
    }
}
