namespace MyRecipes.Web.ViewModels.Recipes
{

    using System.Collections.Generic;

    public class RecipesListViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }

        public int PageNumber { get; set; }
    }
}
