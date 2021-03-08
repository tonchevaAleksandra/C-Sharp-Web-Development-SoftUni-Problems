namespace Panda.Services
{
    public interface IUsersService
    {
        bool IsUsernameAvailable(string username);
        bool IsEmailAvailable(string email);

        void Create(string username, string email, string password);
    }
}
