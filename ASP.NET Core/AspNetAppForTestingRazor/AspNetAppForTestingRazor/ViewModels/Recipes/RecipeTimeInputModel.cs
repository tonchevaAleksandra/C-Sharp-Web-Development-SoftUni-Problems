using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetAppForTestingRazor.ViewModels.Recipes
{
    public class RecipeTimeInputModel : IValidatableObject
    {
        [Range(1, 24 * 60)]
        [Display(Name="Preparation time")]
        public int PreparationTime { get; set; }

        [Range(1, 2 * 24 * 60)]
        [Display(Name = "Cooking time")]
        public int CookingTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.PreparationTime < this.CookingTime)
            {
                yield return new ValidationResult("Prep. time cannot be less then cook time");
            }

            if (this.PreparationTime + this.
                CookingTime > 2.5 * 24 * 60)
            {
                yield return new ValidationResult("Prep time + cook time cannot be more then 2.5 days");
            }
        }
    }
}
