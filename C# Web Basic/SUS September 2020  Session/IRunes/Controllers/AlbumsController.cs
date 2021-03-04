using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Services;
using IRunes.ViewModels.Albums;
using SUS.HTTP;
using SUS.MvcFramework;

namespace IRunes.Controllers
{
   public class AlbumsController:Controller
    {
        private readonly IAlbumsService _albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            _albumsService = albumsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var model = new AllAlbumsViewModel
            {
                Albums = this._albumsService.GetAll()
            };

            return this.View(model);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();

        }

        [HttpPost]
        public HttpResponse Create(AlbumInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Error("Name should be between [4-20] characters.");
            }

            if (String.IsNullOrEmpty(input.Cover))
            {
                return this.Error("Cover is required.");
            }

            this._albumsService.Create(input.Name, input.Cover);

            return this.Redirect("/Albums/All");

        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this._albumsService.GetDetails(id);

            return this.View(viewModel);

        }
    }
}
