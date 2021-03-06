﻿using Andreys.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys.App.Controllers
{

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpGet("/")]
        public HttpResponse Home()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Home/Index");
            }

            var viewModel = this.productsService.GetAllProducts();
            return this.View(viewModel);
        }
    }
}
