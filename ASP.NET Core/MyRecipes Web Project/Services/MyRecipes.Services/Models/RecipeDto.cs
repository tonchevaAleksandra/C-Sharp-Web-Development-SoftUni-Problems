namespace MyRecipes.Services.Models
{
    using System;
    using System.Collections.Generic;

    public class RecipeDto
    {
        public int OriginalRecipeId { get; set; }

        public string RecipeName { get; set; }

        public string CategoryName { get; set; }

        public string Instructions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int PortionsCount { get; set; }

        public string OriginalUrl { get; set; }

        public string Extension { get; set; }

        public ICollection<string> Ingredients { get; set; }
            = new List<string>();

        public ICollection<string> Images { get; set; }
            = new HashSet<string>();
    }
}
