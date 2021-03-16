using AspNetAppForTestingRazor.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AspNetAppForTestingRazor.ModelBinders;
using AspNetAppForTestingRazor.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetAppForTestingRazor.ViewModels.Recipes
{
    public class AddRecipeInputModel
    {

        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [RegularExpression("[A-Z][^_]+")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name="Recipe type")]
        public RecipeType RecipeType { get; set; }

        [ModelBinder(typeof(ExtractYearModelBinder))]

        //[Range(1900, int.MaxValue)]
        [CurrentYearMAxValue(1900)]
        public int Year { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="First tiem the recipe is cooked")]
        public DateTime FirstCooked { get; set; }

        public RecipeTimeInputModel Time { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        //public bool Bool { get; set; }
        public IEnumerable<SelectListItem> Ingredients { get; set; }
    }
}
