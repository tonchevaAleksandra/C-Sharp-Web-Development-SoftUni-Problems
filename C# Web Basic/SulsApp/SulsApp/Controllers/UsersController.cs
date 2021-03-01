﻿using SIS.HTTP;
using SIS.HTTP.Logging;
using SIS.MvcFramework;
using SulsApp.Services;
using SulsApp.ViewModels.Users;
using System;
using System.Net.Mail;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private ILogger logger;

        public UsersController(IUsersService usersService, ILogger logger)
        {
            this.usersService = usersService;
            this.logger = logger;
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
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersService.GetUserId(username, password);
            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userId);
            this.logger.Log("User logged in: " + username);
            return this.Redirect("/");

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

        public HttpResponse Register()
        {
            return this.View();

        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {

            if (input.Password != input.ConfirmedPassword)
            {
                return this.Error("Passwords should be the same.");
            }

            if (input.Username?.Length < 5 || input.Username?.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters.");
            }
            if (input.Password?.Length < 6 || input.Password?.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters.");
            }

            if (!this.IsValid(input.Email))
            {
                return this.Error("Invalid email address.");
            }

            if (this.usersService.IsUsernameUsed(input.Username))
            {
                return this.Error("This username already exist!");
            }

            if (this.usersService.IsEmailUsed(input.Email))
            {
                return this.Error("This email already used!");
            }

            this.usersService.CreateUser(input.Username, input.Email, input.Password);

            this.logger.Log("New user: " + input.Username);

            return this.Redirect("/Users/Login");

        }

        private bool IsValid(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
