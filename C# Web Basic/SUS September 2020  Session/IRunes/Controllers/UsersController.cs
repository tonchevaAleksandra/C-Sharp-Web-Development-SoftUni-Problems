using System;
using System.Collections.Generic;
using System.Text;
using SUS.HTTP;
using SUS.MvcFramework;

namespace IRunes.Controllers
{
  public  class UsersController:Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            return this.View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(string username, string password, string confirmPassword, string email)
        {
            return this.View();
        }
    }
}
