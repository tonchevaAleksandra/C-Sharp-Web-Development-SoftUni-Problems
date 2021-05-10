namespace MyRecipes.Web.ViewModels.Recipes
{
    using System.Linq;

    using AutoMapper;
    using MyRecipes.Data.Models;
    using MyRecipes.Services.Mapping;

    public class RecipeInListViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, RecipeInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                {
                    opt.MapFrom(r => r.Images.FirstOrDefault().Url != null
                        ? r.Images.FirstOrDefault().Url
                        : "/images/recipes/" + r.Images.FirstOrDefault().Id  +
                          r.Images.FirstOrDefault().Extension);
                });
        }
    }
}
