namespace Andreys.Services
{
    public interface IUsersService
    {
        void RegisterUser(string username, string email, string password);
        bool IsUsernameAvailable(string username);
        bool IsEmailAvailable(string email);

        string GetUserId(string username, string password);
    }
}
