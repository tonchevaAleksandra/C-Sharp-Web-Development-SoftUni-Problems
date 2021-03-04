using System.Linq;
using IRunes.Data;
using IRunes.Models;

namespace IRunes.Services
{
    public class TracksService : ITracksService
    {
        private readonly ApplicationDbContext _db;

        public TracksService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Create(string albumId, string name, string link, decimal price)
        {
            var track = new Track()
            {
                AlbumId = albumId,
                Name = name,
                Link = link,
                Price = price,
            };

            this._db.Tracks.Add(track);

            var allTrackPricesSum = this._db.Tracks.Where(x => x.AlbumId == albumId).Sum(x => x.Price);
            var album = this._db.Albums.Find(albumId);
            album.Price = allTrackPricesSum * 0.87M;

            this._db.SaveChanges();
        }
    }
}
