using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Musaca.Data;
using Musaca.Models;

namespace Musaca.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public string GetUserId(string username, string password)
        {
            var hashedPass = this.Hash(password);
            var userId = this.db.Users.Where(x => x.Username == username && x.Password == hashedPass).Select(x => x.Id).FirstOrDefault();

            return userId;
        }

        public void Create(string username, string email, string password)
        {
            var pass = this.Hash(password);
            var user = new User()
            {
                Email = email,
                Username = username,
                Password = pass
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        private string Hash(string password)
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
