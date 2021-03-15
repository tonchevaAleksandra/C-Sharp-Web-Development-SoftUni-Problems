using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetAppForTestingRazor.Controllers
{
    public class InfoController : Controller
    {

        public IActionResult Time()
        {
            return this.Content(DateTime.UtcNow.ToLongTimeString());
        }
        public IActionResult Date()
        {
            return this.Content(DateTime.UtcNow.ToLongDateString());
        }
    }
}
