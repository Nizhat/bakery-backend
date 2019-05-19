using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { ID = 1, Title = "Tomato Soup", Category = "Groceries", Description = "Des...", Price = 1, ImageSrc = "" },
            new Product { ID = 2, Title = "Yo-yo", Category = "Toys", Description = "Des...", Price = 3.75, ImageSrc = "" },
            new Product { ID = 3, Title = "Hammer", Category = "Hardware", Description = "Des...", Price = 16.99, ImageSrc = "" }
        };

        // GET api/products
        [Route("")]
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        // GET api/products/id
        [Route("GetProduct")]
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        // GET api/products/category
        [Route("GetProductsByCategory")]
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return products.Where(
                (p) => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));
        }

        // GET api/products/delete/id
        [Route("DeleteProduct")]
        public IHttpActionResult DeleteProduct(int id) {
            var product = products.FirstOrDefault((p) => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
