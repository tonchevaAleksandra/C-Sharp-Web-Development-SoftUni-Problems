using MyRecipes.Services.Data.Models;

namespace MyRecipes.Services.Data
{
    using System;
    using System.Linq;

    using MyRecipes.Data.Common.Repositories;
    using MyRecipes.Data.Models;
    using MyRecipes.Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IRepository<Image> imageRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Recipe> recipiesRepository;

        public GetCountsService(IDeletableEntityRepository<Category> categoriesRepository, IRepository<Image> imageRepository, IDeletableEntityRepository<Ingredient> ingredientsRepository, IDeletableEntityRepository<Recipe> recipiesRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.imageRepository = imageRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipiesRepository = recipiesRepository;
        }

        public CountsDto GetCounts()
        {
            var data = new CountsDto()
            {
                RecipesCount = this.recipiesRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
                ImagesCount = this.imageRepository.All().Count(),
                IntIngredientsCount = this.ingredientsRepository.All().Count(),
            };

            return data;
        }
    }
}
