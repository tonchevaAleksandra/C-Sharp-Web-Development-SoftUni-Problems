namespace MyRecipes.Web.ViewModels.Recipes
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class RecipeIngredientInputModel
    {
        [DisplayName("Съставка")]
        [Required]
        [MinLength(3)]
        public string IngredientName { get; set; }

        [DisplayName("Количество")]
        [Required]
        [MinLength(1)]
        public string Quantity { get; set; }
    }
}
