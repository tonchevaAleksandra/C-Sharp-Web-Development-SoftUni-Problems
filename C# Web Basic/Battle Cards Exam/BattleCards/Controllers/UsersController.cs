using System;
using BattleCards.Services;
using BattleCards.ViewModels;
using BattleCards.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public HttpResponse Login()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this._usersService.GetUserId(input.Username, input.Password);
            if (userId == null)
            {
                return this.Redirect("Users/Login");
            }

            this.SignIn(userId);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {

            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrEmpty(input.Email))
            {
                return this.Redirect("/Users/Register");
            }

            if (!this._usersService.IsUsernameAvailable(input.Username))
            {
                return this.Redirect("/Users/Register");
            }

            if (input.Username.Length < 5 || input.Username.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }

            if (input.Password.Length<6 || input.Password.Length>20)
            {
                return this.Redirect("/Users/Register");
            }

            if (!this._usersService.IsEmailAvailable(input.Email))
            {
                return this.Redirect("Users/Register");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Redirect("Users/Register");
            }

            this._usersService.Register(input.Username, input.Email, input.Password);

            return this.Redirect("Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("Users/Login");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}
