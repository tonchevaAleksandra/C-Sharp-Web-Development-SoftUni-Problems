namespace MyRecipes.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Data.Models;
    using MyRecipes.Services.Data;
    using MyRecipes.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public RecipesController(ICategoriesService categoriesService, IRecipesService recipesService, UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var imagePath = $"{this.environment.ContentRootPath}\\wwwroot\\img";

            // var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await this.recipesService.CreateAsync(input, userId, imagePath);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError(string.Empty, e.Message);
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            // TODO: CreateAsync recipe using service method
            // TODO: Redirect to recipe info page
            return this.Redirect("/");
        }

        // Recipes/All/1
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 12;
            var viewModel = new RecipesListViewModel()
            {
                ItemsPerPage = itemsPerPage,
                PageNumber = id,
                Recipes = this.recipesService.GetAll<RecipeInListViewModel>(id, itemsPerPage),
                RecipesCount = this.recipesService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}
