using System;
using System.Collections.Generic;
using System.Linq;
using Andreys.Data;
using Andreys.Models;
using Andreys.Models.Enums;
using Andreys.ViewModels.Products;

namespace Andreys.Services
{
    public class ProductsService:IProductsService
    {
        private ApplicationDbContext db;

        public ProductsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string description, string imageUrl, string category, string gender, decimal price)
        {
            var product = new Product()
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Category = Enum.Parse<ProductCategory>(category),
                Gender = Enum.Parse<ProductGender>(gender),
                Price = price
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = this.db.Products.FirstOrDefault(x => x.Id == id);
            this.db.Products.Remove(product);
            this.db.SaveChanges();
        }

        public ProductViewModel GetDetails(int id)
        {
           return this.db.Products.Where(x => x.Id == id).Select(x=> new ProductViewModel()
            {
                Category = x.Category.ToString(),
                Description = x.Description,
                Gender = x.Gender.ToString(),
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price
            }).FirstOrDefault();

        }

        public ICollection<ProductViewModel> GetAllProducts()
        {
            return this.db.Products.Select(x => new ProductViewModel()
            {
                Category = x.Category.ToString(),
                Description = x.Description,
                Gender = x.Gender.ToString(),
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Name = x.Name,
                Price = x.Price
            }).ToList();
        }
    }
}
