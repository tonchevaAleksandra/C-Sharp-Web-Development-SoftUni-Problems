﻿using System;
using System.ComponentModel.DataAnnotations;
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

            this.usersService.Create(model.Username, model.Email, model.Password);

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
