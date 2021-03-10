using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Chushka.Data;
using Chushka.Models;
using SUS.MvcFramework;

namespace Chushka.Services
{
  public  class UsersService:IUsersService
    {
        private ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public string CreateUser(string username,string fullName, string email, string password)
        {
            var hashedPass = ComputeHash(password);

            var user = new User
            {
                Username = username,
                Email = email,
                Password = hashedPass,
                FullName = fullName,
                Role = HasAdmin() ? IdentityRole.User : IdentityRole.Admin
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();

            return user.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        public string GetUserId(string username, string password)
        {
            var hashedPass = ComputeHash(password);
            return this.db.Users.Where(x => x.Username == username && x.Password == hashedPass).Select(x=>x.Id).FirstOrDefault();
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        public bool HasAdmin()
        {
           return this.db.Users.Any(x => x.Role == IdentityRole.Admin);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
