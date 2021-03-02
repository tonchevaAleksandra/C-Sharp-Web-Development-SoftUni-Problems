using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SulsApp.Models;

namespace SulsApp.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateUser(string username, string email, string password)
        {
            var user = new User()
            {
                Email = email,
                Username = username,
                Password = this.Hash(password)
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Username == username && x.Password == this.Hash(password));

            return user?.Id;
        }

        public void ChangePassword(string username, string newPassword)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Username == username);
            if (user==null)
            {
                return;
            }
            user.Password = this.Hash(newPassword);
            this.db.SaveChanges();
        }

        public bool IsUsernameUsed(string username)
        {
            return this.db.Users.Any(x => x.Username == username);
        }

        public bool IsEmailUsed(string email)
        {
            return this.db.Users.Any(x => x.Email == email);
        }

        public int CountUsers()
        {
            return this.db.Users.Count();
        }
        private string Hash(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return null;
            }

            var crypto = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] hashBytes = crypto.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (var hashByte in hashBytes)
            {
                hash.Append(hashByte.ToString("x2"));
            }

            return hash.ToString();
        }

    }
}
