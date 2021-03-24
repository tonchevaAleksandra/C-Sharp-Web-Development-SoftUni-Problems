using System;
using System.ComponentModel.DataAnnotations;
using AspNetAppForTestingRazor.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetAppForTestingRazor.Models
{
    public class Product
    {
        //[Range(1,10000)]
        public int Id { get; set; }

        [Required]
        //[MinLength(10)]
        public string Name { get; set; }

        [MinLength(10)]
        public string Description { get; set; }

        [CurrentYearMAxValue(1990)]
        public DateTime ActiveFrom { get; set; }

        [Range(0,10000)]
        public double Price { get; set; }
    }
}
