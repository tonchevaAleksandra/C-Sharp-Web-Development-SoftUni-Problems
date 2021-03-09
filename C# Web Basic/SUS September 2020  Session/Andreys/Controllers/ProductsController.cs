using System;
using Andreys.Services;
using Andreys.ViewModels.Products;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Home/Index");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddProductInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (String.IsNullOrEmpty(model.Name) || model.Name.Length< 4 || model.Name.Length> 20)
            {
                return this.Redirect("/Products/Add");
                //return this.Error("Name of the product should be between [4-20] characters.");
            }

            if (!String.IsNullOrEmpty(model.Description) && model.Description.Length>10)
            {
                return this.Redirect("/Products/Add");
                //return this.Error("Description cannot exceed 10 characters.");
            }

            if (model.Price<=0M)
            {
                return this.Redirect("/Products/Add");
                //return this.Error("Price cannot be zero or negative number.");
            }

            if (String.IsNullOrEmpty(model.Category))
            {
                return this.Redirect("/Products/Add");
                //return this.Error("Category cannot be empty.");
            }

            if (String.IsNullOrEmpty(model.Gender))
            {
                return this.Redirect("/Products/Add");
                //return this.Error("Gender cannot be empty.");
            }

            this.productsService.Create(model.Name, model.Description, model.ImageUrl,model.Category, model.Gender, model.Price);
            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Home/Index");
            }

            var viewModel = this.productsService.GetDetails(id);

            return this.View(viewModel);
        }

        public HttpResponse Delete(int id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Home/Index");
            }

            this.productsService.DeleteProduct(id);

            return this.Redirect("/");
        }
    }
}
