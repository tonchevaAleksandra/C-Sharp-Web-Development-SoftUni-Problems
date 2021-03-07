using System;
using System.ComponentModel.DataAnnotations;

namespace Musaca.Models
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
