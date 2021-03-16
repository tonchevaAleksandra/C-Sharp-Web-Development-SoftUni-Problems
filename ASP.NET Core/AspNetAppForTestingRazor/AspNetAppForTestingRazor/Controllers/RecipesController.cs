using AspNetAppForTestingRazor.ViewModels.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetAppForTestingRazor.Controllers
{
    public class RecipesController : Controller
    {
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(/*[FromForm]*/ /*[FromQuery]*/ AddRecipeInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            //var count= this.HttpContext.Request.Cookies.Keys.Count;
            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
