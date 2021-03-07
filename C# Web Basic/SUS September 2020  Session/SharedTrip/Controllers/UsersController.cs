using System;
using System.ComponentModel.DataAnnotations;
using SharedTrip.Services;
using SharedTrip.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService  usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserViewModel model)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(model.Username, model.Password);

            if (userId==null)
            {
                return this.Error("Invalid username ot password.");

            }

            this.SignIn(userId);

            return this.Redirect("/Trips/All");
        }
        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterViewModel model)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            if (model.Username.Length<5 || model.Username.Length>20)
            {
                return this.Error("Username should be between [5-20] characters.");
            }

            if (String.IsNullOrEmpty(model.Email) || !new EmailAddressAttribute().IsValid(model.Email))
            {
                return this.Error("Invalid email.");
            }

            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.Error("The passwords should be between [6-20] characters.");
            }

            if (model.Password!=model.ConfirmPassword)
            {
                return this.Error("The passwords do not match.");

            }

            if (!this.usersService.IsUsernameAvailable(model.Username))
            {
                return this.Error("The username already exist.");
            }

            if (!this.usersService.IsEmailAvailable(model.Email))
            {
                return this.Error("Email already exist.");
            }

            this.usersService.Create(model.Username, model.Email, model.Password);

            return this.Redirect("/Users/Login");

        }

        public HttpResponse Logout()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
