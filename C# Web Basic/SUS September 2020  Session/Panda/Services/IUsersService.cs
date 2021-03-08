using System.Collections.Generic;

namespace Panda.Services
{
    public interface IUsersService
    {
        bool IsUsernameAvailable(string username);
        bool IsEmailAvailable(string email);

        void Create(string username, string email, string password);

        string GetUserId(string username, string password);

        string GetUsername(string id);
        string GetUserIdByUsername(string username);

        ICollection<string> GetAllUsernames();
    }
}
