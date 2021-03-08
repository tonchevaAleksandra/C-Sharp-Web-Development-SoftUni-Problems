using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Migrations;
using Musaca.Services;
using Musaca.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Musaca.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IOrdersService ordersService;


        public UsersController(IUsersService usersService, IOrdersService ordersService)
        {
            this.usersService = usersService;
            this.ordersService = ordersService;
          
        }
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(model.Username, model.Password);

            if (userId==null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrEmpty(model.Username) || model.Username.Length<5 || model.Username.Length>20)
            {
                return this.Redirect("/Users/Register");
            }

            if (String.IsNullOrEmpty(model.Email) || model.Email.Length<5 || model.Email.Length>20 || !new EmailAddressAttribute().IsValid(model.Email))
            {
                return this.Redirect("/Users/Register");
            }

            if (model.Password!= model.ConfirmPassword)
            {
                return this.Redirect("/Users/Register");
            }

            if (!this.usersService.IsUsernameAvailable(model.Username))
            {
                return this.Redirect("/Users/Register");
            }

            if (!this.usersService.IsEmailAvailable(model.Email))
            {
                return this.Redirect("/Users/Register");
            }

            var userId = this.usersService.Create(model.Username, model.Email, model.Password);
            this.ordersService.Create(userId);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Profile()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
