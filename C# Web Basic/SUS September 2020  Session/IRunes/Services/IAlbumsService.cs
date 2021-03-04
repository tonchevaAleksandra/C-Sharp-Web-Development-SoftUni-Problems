using System.Collections.Generic;
using IRunes.ViewModels.Albums;

namespace IRunes.Services
{
    public interface IAlbumsService
    {
        void Create(string name, string cover);

        ICollection<AlbumViewModel> GetAll();

        AlbumDetailsViewModel GetDetails(string id);
    }
}
