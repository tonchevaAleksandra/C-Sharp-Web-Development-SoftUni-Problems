namespace IRunes.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);
        void Register(string username, string email, string password);
        bool IsUsernameAvailable(string username);
        bool IsEmailAvailable(string email);
        string GetUsername(string id);
    }
}
