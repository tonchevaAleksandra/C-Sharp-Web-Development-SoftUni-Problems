using System;
using AspNetAppForTestingRazor.ViewModels.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetAppForTestingRazor.Controllers
{
    public class RecipesController : Controller
    {
        public IActionResult Add()
        {
            var model = new AddRecipeInputModel()
            {
                Date = DateTime.UtcNow,
                Time = new RecipeTimeInputModel()
                {
                    CookingTime = 20,
                    PreparationTime = 30
                },
                Name = "Banica",
                FirstCooked = DateTime.UtcNow.AddYears(+130)
            };
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Add(/*[FromForm]*/ /*[FromQuery]*/ AddRecipeInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            //var count= this.HttpContext.Request.Cookies.Keys.Count;
            return this.RedirectToAction(nameof(ThankYou));
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
