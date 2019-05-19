using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/Products1")]
    public class Products1Controller : ApiController
    {
        private ProductsContext db = new ProductsContext();
        // Typed lambda expression for Select() method. 
        private static readonly Expression<Func<Product, ProductDto>> AsProductDto =
            x => new ProductDto
            {
                ID = x.ID,
                Title = x.Title,
                Category = x.Category,
                Description = x.Description,
                ImageSrc = x.ImageSrc,
                Price = x.Price
            };

        // GET: api/Products1
        [Route("")]
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Products1/5
        [Route("GetProduct")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            ProductDto product = await db.Products
               .Where(b => b.ID == id)
               .Select(AsProductDto)
               .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET: api/Products1/GetProductsByCategory
        [Route("GetProductsByCategory")]
        [ResponseType(typeof(Product))]
        public IQueryable<ProductDto> GetProductsByCategory(string category)
        {
            return db.Products.Include(b => b.Category)
                .Where(b => b.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).Select(AsProductDto);
        }

        // GET: api/Products1/GetRandomThreeProducts
        [Route("GetRandomThreeProducts")]
        [ResponseType(typeof(Product))]
        public IQueryable<Product> GetRandomThreeProducts()
        {
            Random rnd = new Random();
            int skipNumber = rnd.Next(1, (db.Products.Count()-3));
            return db.Products.OrderBy(p => p.ID)
                .Skip(skipNumber)
                .Take(3);
        }

        // PUT: api/Products1/5
        [Route("PutProduct")]
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(PutProductModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item.id != item.Product.ID)
            {
                return BadRequest();
            }

            db.Entry(item.Product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(item.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products1
        [Route("PostProduct")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return Ok(product);//return CreatedAtRoute("DefaultApi", new { id = product.ID }, product);
        }

        // DELETE: api/Products1/5
        [Route("DeleteProduct")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}