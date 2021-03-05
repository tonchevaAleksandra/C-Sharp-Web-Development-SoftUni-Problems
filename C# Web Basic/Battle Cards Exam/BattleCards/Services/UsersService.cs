using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BattleCards.Data;
using BattleCards.Models;

namespace BattleCards.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _db;

        public UsersService(ApplicationDbContext db)
        {
            _db = db;
        }
        public string GetUserId(string username, string password)
        {
            var pass = Hash(password);
            return this._db.Users.Where(x => x.Username == username && x.Password == pass).Select(x => x.Id)
                .FirstOrDefault();
        }

        public void Register(string username, string email, string password)
        {
            var pass = Hash(password);
            var user = new User()
            {
                Email = email,
                Password = pass,
                Username = username
            };

            this._db.Users.Add(user);
            this._db.SaveChanges();
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this._db.Users.Any(x => x.Username == username);
        }

        public bool IsEmailAvailable(string email)
        {
            return !this._db.Users.Any(x => x.Email == email);
        }

        public string GetUsername(string id)
        {
            return this._db.Users.Where(x => x.Id == id).Select(x => x.Username).FirstOrDefault();
        }

        private static string Hash(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
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
