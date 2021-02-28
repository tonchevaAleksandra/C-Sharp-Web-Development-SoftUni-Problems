using System;
using System.Security.Cryptography;
using System.Text;

namespace SulsApp.Services
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsValidUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(string username, string newPassword)
        {
            throw new NotImplementedException();
        }

        public int CountUsers()
        {
            throw new NotImplementedException();
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
