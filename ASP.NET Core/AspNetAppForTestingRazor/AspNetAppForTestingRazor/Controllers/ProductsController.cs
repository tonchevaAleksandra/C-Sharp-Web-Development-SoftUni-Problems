using AspNetAppForTestingRazor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetAppForTestingRazor.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AspNetAppForTestingRazor.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
           return this._db.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = this._db.Products.Find(id);
            if (product==null)
            {
                return this.NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Product product)
        {
            await this._db.Products.AddAsync(product);
            await this._db.SaveChangesAsync();

            return this.CreatedAtAction("Get", new {id = product.Id}, product);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Product product)
        {
            this._db.Entry(product).State = EntityState.Modified;
            await this._db.SaveChangesAsync();
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = this._db.Products.Find(id);
            if (product==null)
            {
                return this.NotFound();
            }
            this._db.Remove(product);
          await  this._db.SaveChangesAsync();
          return this.NoContent();
        }

        //[HttpGet]
        //public Product Test()
        //{
        //    return new Product()
        //    {
        //        ActiveFrom = DateTime.UtcNow,
        //        Description = "description",
        //        Price = 123.45,
        //        Id = 123,
        //        Name = "name"
        //    };
        //}

        //[HttpDelete]
        //public string SomeMethod()
        //{
        //    return "DELETE";
        //}

        //[HttpPost]
        ////[Authorize]
        //public ActionResult<Product> PostMethod(Product product, int id )
        //{
        //    if (id<1)
        //    {
        //        return this.NotFound();
        //    }
        //    product.Id = id;
        //    return product;
        //}
    }
}
