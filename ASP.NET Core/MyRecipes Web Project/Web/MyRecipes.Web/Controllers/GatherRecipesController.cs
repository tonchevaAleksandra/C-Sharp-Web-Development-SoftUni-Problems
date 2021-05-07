using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using MyRecipes.Services;

namespace MyRecipes.Web.Controllers
{
    public class GatherRecipesController:BaseController
    {
        private readonly IScraperService scraperService;

        public GatherRecipesController(IScraperService scraperService)
        {
            this.scraperService = scraperService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add()
        {
           await this.scraperService.PopulateDbWithRecipesAsync();
           return this.Redirect("/");
        }
    }
}
