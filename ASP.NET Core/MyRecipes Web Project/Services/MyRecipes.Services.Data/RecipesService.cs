namespace MyRecipes.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;
    using MyRecipes.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;

        public RecipesService(IDeletableEntityRepository<Recipe> recipesRepository, IDeletableEntityRepository<Ingredient> ingredientsRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task CreateAsync(CreateRecipeInputModel input, string userId)
        {
            var recipe = new Recipe()
            {
                CategoryId = input.CategoryId,
                CookingTime = TimeSpan.FromMinutes(input.CookingTime),
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
                Instructions = input.Instructions,
                Name = input.Name,
                PortionCount = input.PortionsCount,
                CreatedByUserId = userId,
            };

            foreach (var inputIngredient in input.Ingredients)
            {
                var ingredient = this.ingredientsRepository.All().FirstOrDefault(x => x.Name == inputIngredient.IngredientName);

                if (ingredient == null)
                {
                    ingredient = new Ingredient() { Name = inputIngredient.IngredientName };
                }

                recipe.Ingredients.Add(new RecipeIngredient()
                {
                    Ingredient = ingredient,
                    Quantity = inputIngredient.Quantity,
                });
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
