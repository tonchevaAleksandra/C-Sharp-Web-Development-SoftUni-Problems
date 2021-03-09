using System.Collections.Generic;
using Andreys.ViewModels.Products;

namespace Andreys.Services
{
    public interface IProductsService
    {
        void Create(string name, string description, string imageUrl, string category, string gender, decimal price);

        void DeleteProduct(int id);

        ProductViewModel GetDetails(int id);

        ICollection<ProductViewModel> GetAllProducts();
    }
}
