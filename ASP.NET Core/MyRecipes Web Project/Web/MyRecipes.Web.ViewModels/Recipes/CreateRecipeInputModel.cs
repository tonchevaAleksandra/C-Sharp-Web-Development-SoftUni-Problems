using System.ComponentModel;

namespace MyRecipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyRecipes.Data.Models;

    public class CreateRecipeInputModel
    {
        [DisplayName("Име на рецептата")]
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [DisplayName("Инструкции")]
        [Required]
        [MinLength(100)]
        public string Instructions { get; set; }

        [DisplayName("Време за подготовка")]
        [Range(0, 24 * 60)]
        [Display(Name="Preparation time (in minutes)")]
        public int PreparationTime { get; set; }

        [DisplayName("Време за готвене")]
        [Range(0, 24 * 60)]
        [Display(Name = "Cooking time (in minutes)")]
        public int CookingTime { get; set; }

        [DisplayName("Порции")]
        [Range(1, 100)]
        public int PortionsCount { get; set; }

        // TODO: ImageUrl to original site
        public string CreatedByUserId { get; set; }

        [DisplayName("Категория")]
        public int CategoryId { get; set; }

        [DisplayName("Съставки")]
        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
