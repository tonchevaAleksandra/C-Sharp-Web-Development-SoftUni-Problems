using IRunes.Services;
using IRunes.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace IRunes.Controllers
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
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            var userId = this.usersService.GetUserId(input.Username, input.Password);
            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (String.IsNullOrEmpty(model.Email))
            {
                return this.Error("Email cannot be null.");
            }
            if (model.Username.Length < 4 || model.Username.Length > 20)
            {
                return this.Error("Username should be between [4-20] characters.");
            }

            if (model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.Error("Password should be between [6-20] characters.");
            }
            if (model.Password != model.ConfirmPassword)
            {
                return this.Error("Passwords should match.");
            }

            if (!usersService.IsUsernameAvailable(model.Username))
            {
                return this.Error("This username already exists.");
            }

            if (!this.usersService.IsEmailAvailable(model.Email))
            {
                return this.Error("This email already exists.");
            }

            this.usersService.Register(model.Username, model.Email, model.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
