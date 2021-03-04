namespace IRunes.Services
{
    public interface ITracksService
    {
        void Create(string albumId,string name, string link, decimal price);
    }
}
