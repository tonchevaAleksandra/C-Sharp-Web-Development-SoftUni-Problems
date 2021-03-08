using Musaca.Data;
using Musaca.Models;

namespace Musaca.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext db;

        public ProductsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string name, decimal price)
        {
            var product = new Product
            {
                Name = name,
                Price = price
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }
    }
}
