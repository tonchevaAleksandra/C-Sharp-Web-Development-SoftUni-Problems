using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetAppForTestingRazor.ValidationAttributes
{
    public class CurrentYearMAxValueAttribute : ValidationAttribute
    {
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    validationContext.GetService()
        //    return base.IsValid(value, validationContext);
        //}
        public CurrentYearMAxValueAttribute(int minYear)
        {
            this.MinYear = minYear;
            this.ErrorMessage = $"Value should be between: {minYear} - {DateTime.UtcNow.Year}";
        }

        public int MinYear { get; }
        public override bool IsValid(object value)
        {
            if (value is int intValue)
            {
                if (intValue <= DateTime.UtcNow.Year && intValue >= this.MinYear)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
