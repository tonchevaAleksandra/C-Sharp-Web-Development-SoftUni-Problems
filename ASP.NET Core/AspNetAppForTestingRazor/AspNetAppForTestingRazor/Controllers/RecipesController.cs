using System;
using AspNetAppForTestingRazor.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AspNetAppForTestingRazor.Controllers
{

    public class RecipeTimeInputModel
    {
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
    }
    public class AddRecipeInputModel
    {
        //public int Id { get; set; }
        //public string Name { get; set; }

        [ModelBinder(typeof(ExtractYearModelBinder))]
        public int Year { get; set; }

        public DateTime FirstCooked { get; set; }
        //public RecipeTimeInputModel Time { get; set; }
        //public DateTime Date { get; set; }
        //public bool Bool { get; set; }
        //public string[] Ingredients { get; set; }
    }
    
    public class RecipesController : Controller
    {
        public IActionResult Add(/*[FromForm]*/ /*[FromQuery]*/ AddRecipeInputModel input)
        {
          //var count= this.HttpContext.Request.Cookies.Keys.Count;
            return this.Json(input);
        }
    }
}
