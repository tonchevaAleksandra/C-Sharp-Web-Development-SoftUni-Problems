using System;
using System.ComponentModel.DataAnnotations;
using Panda.Services;
using Panda.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Panda.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
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
        public HttpResponse Login(LoginInputModel input)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(input.Username, input.Password);
            if (userId==null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userId);
            return this.Redirect("/");
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
        public HttpResponse Register(RegisterInputModel input)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrEmpty(input.Username) || input.Username.Length<5 || input.Username.Length>20)
            {
                return this.Register();
            }
            if (String.IsNullOrEmpty(input.Email) || input.Email.Length < 5 || input.Email.Length > 20 || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Register();
            }
            if (String.IsNullOrEmpty(input.Password))
            {
                return this.Register();
            }

            if (input.Password!=input.ConfirmPassword)
            {
                return this.Register();
            }

            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                return this.Register();
            }

            if (!this.usersService.IsEmailAvailable(input.Email))
            {
                return this.Register();
            }

            this.usersService.Create(input.Username, input.Email, input.Password);

            return this.Redirect("/Users/Login");
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
