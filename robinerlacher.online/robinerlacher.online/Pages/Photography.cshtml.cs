using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace robinerlacher.online.Pages
{
    // TODO -> open images in a dialog box!

    public class PhotographyModel : PageModel
    {
        public void OnGet()
        {
            var test = String.Format(robinerlacher.online.Lang.IndexWelcomeText, Environment.NewLine);
        }
    }
}
