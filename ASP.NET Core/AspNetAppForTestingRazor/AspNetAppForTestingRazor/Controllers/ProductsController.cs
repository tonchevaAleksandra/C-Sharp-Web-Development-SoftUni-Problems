using AspNetAppForTestingRazor.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetAppForTestingRazor.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public Product Test()
        {
            return new Product()
            {
                ActiveFrom = DateTime.UtcNow,
                Description = "description",
                Price = 123.45,
                Id = 123,
                Name = "name"
            };
        }

        [HttpDelete]
        public string SomeMethod()
        {
            return "DELETE";
        }

        [HttpPost]
        public ActionResult<Product> PostMethod(Product product, int id )
        {
            if (id<1)
            {
                return this.NotFound();
            }
            product.Id = id;
            return product;
        }
    }
}
