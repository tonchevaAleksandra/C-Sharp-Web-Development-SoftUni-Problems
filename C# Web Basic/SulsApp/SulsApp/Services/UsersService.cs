using System;
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
                Username = username,
                Email = email,
                Password = this.Hash(password)
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public bool IsValidUser(string username, string password)
        {
            var passwordHash = this.Hash(password);
            return this.db.Users.Any(x => x.Username == username && x.Password == passwordHash);
        }

        public void ChangePassword(string username, string newPassword)
        {
            var user = this.db.Users.FirstOrDefault(x =>
                x.Username == username/* && x.Password == this.Hash(newPassword)*/);

            if (user==null)
            {
                return;
            }

            user.Password = this.Hash(newPassword);
            this.db.SaveChanges();

        }

        public int CountUsers()
        {
            return this.db.Users.Count();
        }

        private string Hash(string input)
        {
            var crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));

            foreach (byte cryptoByte in crypto)
            {
                hash.Append(cryptoByte.ToString("x2")); //255 => FF
            }

            return hash.ToString();
        }
    }
}
