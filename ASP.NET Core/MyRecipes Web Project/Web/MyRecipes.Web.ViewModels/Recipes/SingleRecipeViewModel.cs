﻿namespace MyRecipes.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using MyRecipes.Data.Models;
    using MyRecipes.Services.Mapping;

    public class SingleRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Instructions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int PortionCount { get; set; }

        public string OriginalUrl { get; set; }

        public string CreatedByUserUserName { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryRecipesCount { get; set; }

        public double AverageVote { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, SingleRecipeViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                {
                    opt.MapFrom(r => r.Images.FirstOrDefault().Url != null
                        ? r.Images.FirstOrDefault().Url
                        : "/images/recipes/" + r.Images.FirstOrDefault().Id + "." +
                          r.Images.FirstOrDefault().Extension);
                })
                .ForMember(x => x.AverageVote, opt =>
                    opt.MapFrom(x => x.Votes.Count == 0 ? 0 : x.Votes.Average(v => v.Value)));
        }
    }
}
