using System;

namespace AspNetAppForTestingRazor.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ActiveFrom { get; set; }
        public double Price { get; set; }
    }
}
