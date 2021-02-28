﻿using System;
using System.Net.Mail;
using System.Security.Cryptography;
using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SulsApp.Models;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    {

        public HttpResponse Login()
        {
            return this.View();

        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            return this.View();

        }

        public HttpResponse Register()
        {
            return this.View();

        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister()
        {
            var username = this.Request.FormData["username"];
            var email = this.Request.FormData["email"];
            var password = this.Request.FormData["password"];
            var confirmPassword = this.Request.FormData["confirmPassword"];
            if (password != confirmPassword)
            {
                return this.Error("Passwords should be the same.");
            }

            if (username?.Length < 5 || username?.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters.");
            }
            if (password?.Length < 6 || password?.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters.");
            }

            if (!this.IsValid(email))
            {
                return this.Error("Invalid email address.");
            }

            var user = new User()
            {
                Username = username,
                Email = email,
                Password = this.Hash(password)
            };
            var db = new ApplicationDbContext();
            db.Users.Add(user);
            db.SaveChanges();

            //TODO: log in
        
            return this.Redirect("/");

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
