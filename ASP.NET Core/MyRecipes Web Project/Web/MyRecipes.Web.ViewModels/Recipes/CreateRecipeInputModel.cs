using System.ComponentModel.DataAnnotations;

namespace MyRecipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;

    using MyRecipes.Data.Models;

    public class CreateRecipeInputModel
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [MinLength(100)]
        public string Instructions { get; set; }
  
        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        [Range(1, 100)]
        public int PortionCount { get; set; }

        // TODO: Url to original site
        public string CreatedByUserId { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }
    }
}
