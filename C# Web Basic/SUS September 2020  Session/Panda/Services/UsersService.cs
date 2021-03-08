using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Panda.Data;
using Panda.Models;

namespace Panda.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public bool IsUsernameAvailable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        public void Create(string username, string email, string password)
        {
            var hashedPass = Hash(password);
            var user = new User
            {
                Username = username,
                Email = email,
                Password = hashedPass
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var hashedPass = Hash(password);
            var userId = this.db.Users.Where(x => x.Username == username && x.Password == hashedPass)
                .Select(x => x.Id).FirstOrDefault();

            return userId;
        }

        public string GetUserIdByUsername(string username)
        {
            var userId = this.db.Users.Where(x => x.Username == username )
                .Select(x => x.Id).FirstOrDefault();

            return userId;
        }

        public ICollection<string> GetAllUsernames()
        {
            return this.db.Users.Select(x => x.Username).ToList();
        }

        public string GetUsername(string id)
        {
            return this.db.Users.Where(x => x.Id == id).Select(x => x.Username).FirstOrDefault();
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
