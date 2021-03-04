using IRunes.Services;
using IRunes.ViewModels.Tracks;
using SUS.HTTP;
using SUS.MvcFramework;

namespace IRunes.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITracksService _tracksService;

        public TracksController(ITracksService tracksService)
        {
            _tracksService = tracksService;
        }
        public HttpResponse Create(string albumId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var viewModel = new CreateViewModel()
            {
                AlbumId = albumId
            };
          
            return this.View(viewModel);
        }
        [HttpPost]
        public HttpResponse Create(TrackInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Error("Track name should be between [4-20] characters.");
            }

            if (!input.Link.StartsWith("http"))
            {
                return this.Error("This is not valid link.");
            }

            if (input.Price < 0)
            {
                return this.Error("Price should be a positive number.");
            }

            this._tracksService.Create(input.AlbumId, input.Name, input.Link, input.Price);

            return this.Redirect("/Albums/Details?id=" + input.AlbumId);
        }

        public HttpResponse Details(string trackId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

          
            var vieaModel = this._tracksService.GetDetails(trackId);
            if (vieaModel==null)
            {
                return this.Error("This track not exist.");
            }

            return this.View(vieaModel);
        }
    }
}
