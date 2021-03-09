using Andreys.Data;

namespace Andreys.Services
{
    public class ProductsService
    {
        private ApplicationDbContext db;

        public ProductsService(ApplicationDbContext db)
        {
            this.db = db;
        }
    }
}
