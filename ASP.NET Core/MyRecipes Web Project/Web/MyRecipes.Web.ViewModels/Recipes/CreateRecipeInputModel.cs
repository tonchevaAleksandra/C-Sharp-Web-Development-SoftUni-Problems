namespace MyRecipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CreateRecipeInputModel /*: IMapTo<Recipe>, IHaveCustomMappings*/
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
        [Display(Name = "Preparation time (in minutes)")]
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

        // public void CreateMappings(IProfileExpression configuration)
        // {
        //   configuration.CreateMap<CreateRecipeInputModel, Recipe>()
        //       .ForMember(x=> x.CookingTime, opt =>
        //       {
        //           opt.MapFrom(cr=> TimeSpan.FromMinutes(cr.CookingTime));
        //       })
        //       .ForMember(x => x.PreparationTime, opt =>
        //       {
        //           opt.MapFrom(cr => TimeSpan.FromMinutes(cr.PreparationTime));
        //       });
        // }
    }
}
