using System;
using System.ComponentModel.DataAnnotations;
using Andreys.Services;
using Andreys.ViewModels;
using Andreys.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys.Controllers
{
    public class UsersController : Controller
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
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

            if (String.IsNullOrEmpty(model.Username))
            {
                return this.Error("Username cannot be empty.");
            }

            var userId = this.usersService.GetUserId(model.Username, model.Password);
            if (userId==null)
            {
                return this.Error("not existing user.");
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
            if (String.IsNullOrEmpty(model.Username) || model.Username.Length<4 || model.Username.Length>10)
            {
                return this.Error("Username should be between [4-10] characters.");
            }

            if (!this.usersService.IsUsernameAvailable(model.Username))
            {
                return this.Error("Username is not available.");
            }

            if (!String.IsNullOrEmpty(model.Email)  && !new EmailAddressAttribute().IsValid(model.Email) || !this.usersService.IsEmailAvailable(model.Email))
            {
                return this.Error("Email address is not valid or available.");
            }

            if (model.Password!=model.ConfirmPassword )
            {
                return this.Error("Passwords should match.");
            }

            this.usersService.RegisterUser(model.Username, model.Email, model.Password);

            return this.Redirect("/");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();

            return this.Redirect("/Home/Index");
        }
    }
}
