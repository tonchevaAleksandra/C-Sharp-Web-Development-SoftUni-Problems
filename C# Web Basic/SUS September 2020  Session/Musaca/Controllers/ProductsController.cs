using System;
using Musaca.Services;
using Musaca.ViewModels.Products;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Musaca.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
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
        public HttpResponse Create(CreateProductViewModel model)
        {
            if (String.IsNullOrEmpty(model.Name) || model.Name.Length<3 || model.Name.Length>10)
            {
                return this.Redirect("/Products/Create");
            }

            if (model.Price<0.01M)
            {
                return this.Redirect("/Products/Create");
            }

            this.productsService.Create(model.Name, model.Price);
            return this.Redirect("/Products/All");
        }

        public HttpResponse All()
        {
            return this.View();
        }
    }
}
