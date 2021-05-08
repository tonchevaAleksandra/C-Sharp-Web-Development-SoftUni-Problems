﻿namespace MyRecipes.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class RecipesListViewModel : PagingViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }
    }
}
