using AspNetAppForTestingRazor.Models;
using System;
using System.ComponentModel.DataAnnotations;
using AspNetAppForTestingRazor.ModelBinders;
using AspNetAppForTestingRazor.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetAppForTestingRazor.ViewModels.Recipes
{
    public class AddRecipeInputModel
    {

        [Required]
        [MinLength(5)]
        [RegularExpression("[A-Z][^_]+")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public RecipeType RecipeType { get; set; }

        [ModelBinder(typeof(ExtractYearModelBinder))]

        //[Range(1900, int.MaxValue)]
        [CurrentYearMAxValue(1900)]
        public int Year { get; set; }

        public DateTime FirstCooked { get; set; }
        public RecipeTimeInputModel Time { get; set; }
        public DateTime Date { get; set; }
        //public bool Bool { get; set; }
        //public string[] Ingredients { get; set; }
    }
}
