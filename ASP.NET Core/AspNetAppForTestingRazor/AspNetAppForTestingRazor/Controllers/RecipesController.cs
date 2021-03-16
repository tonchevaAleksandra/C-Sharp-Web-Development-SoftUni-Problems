using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetAppForTestingRazor.Controllers
{

    public class RecipeTimeInputModel
    {
        [Range(1, 24 * 60)]
        public int PreparationTime { get; set; }

        [Range(1, 2 * 24 * 60)]
        public int CookingTime { get; set; }
    }
    public class AddRecipeInputModel
    {

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [RegularExpression("[A-Z][^_]+")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        //public RecipeType RecipeType { get; set; }
        ////[ModelBinder(typeof(ExtractYearModelBinder))]
        //public int Year { get; set; }

        //public DateTime FirstCooked { get; set; }
        //public RecipeTimeInputModel Time { get; set; }
        //public DateTime Date { get; set; }
        //public bool Bool { get; set; }
        //public string[] Ingredients { get; set; }
    }

    public enum RecipeType
    {
        Unknown = 0,
        FastFood = 1,
        LongCookingMeal = 2
    }

    public class RecipesController : Controller
    {
        public IActionResult Add(/*[FromForm]*/ /*[FromQuery]*/ AddRecipeInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.Json(ModelState);
            }

            //var count= this.HttpContext.Request.Cookies.Keys.Count;
            return this.Json(input);
        }
    }
}
