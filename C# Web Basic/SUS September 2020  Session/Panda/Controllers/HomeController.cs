using System;
using System.Collections.Generic;
using System.Text;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Panda.Controllers
{
  public  class HomeController:Controller
    {
        [HttpGet("/")]
        public HttpResponse IndexSlash()
        {
            return this.Index();
        }

        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.IndexLoggedIn();
            }
            return this.View();
        }

        public HttpResponse IndexLoggedIn()
        {
            return this.View();
        }
    }
}
