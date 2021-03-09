using System;
using Andreys.Services;
using Andreys.ViewModels.Products;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        public HttpResponse Add(AddProductInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrEmpty(model.Name) || model.Name.Length<4 || model.Name.Length>20)
            {
                return this.Error("Name of the product should be between [4-20] characters.");
            }

            if (!String.IsNullOrEmpty(model.Description) && model.Description.Length>10)
            {
                return this.Error("Description cannot exceed 10 characters.");
            }

            if (model.Price<=0M)
            {
                return this.Error("Price cannot be zero or negative number.");
            }

            if (String.IsNullOrEmpty(model.Category))
            {
                return this.Error("Category cannot be empty.");
            }

            if (String.IsNullOrEmpty(model.Gender))
            {
                return this.Error("Gender cannot be empty.");
            }

            return this.Redirect("/");
        }
    }
}
