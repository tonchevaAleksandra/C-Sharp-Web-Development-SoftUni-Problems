using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using IRunes.Data;
using IRunes.Models;
using IRunes.ViewModels.Albums;
using IRunes.ViewModels.Tracks;

namespace IRunes.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly ApplicationDbContext db;

        public AlbumsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string name, string cover)
        {
            var album = new Album
            {
                Name = name,
                Cover = cover,
                Price = 0M
            };

            this.db.Albums.Add(album);
            this.db.SaveChanges();
        }

        public ICollection<AlbumViewModel> GetAll()
        {
            return this.db.Albums.Select(x => new AlbumViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }

        public AlbumDetailsViewModel GetDetails(string id)
        {
            var album = this.db.Albums.Where(x => x.Id == id).Select(x => new AlbumDetailsViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Cover = x.Cover,
                Price = x.Price,
                Tracks = x.Tracks.Select(y => new TrackInfoViewModel()
                {
                    Id = y.Id,
                    Name = y.Name
                }).ToList()
            })
                .FirstOrDefault();

            return album;
        }
    }
}
