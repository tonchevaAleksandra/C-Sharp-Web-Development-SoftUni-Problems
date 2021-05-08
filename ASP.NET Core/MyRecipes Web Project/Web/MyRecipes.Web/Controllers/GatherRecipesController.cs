namespace MyRecipes.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Services;

    public class GatherRecipesController : BaseController
    {
        private readonly IScraperService scraperService;

        public GatherRecipesController(IScraperService scraperService)
        {
            this.scraperService = scraperService;
        }

        [Authorize("Admin")]
        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize("Admin")]
        public async Task<IActionResult> Add()
        {
            await this.scraperService.PopulateDbWithRecipesAsync();
            return this.Redirect("/");
        }
    }
}
