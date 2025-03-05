using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using phonezone_backend.Data;
using phonezone_backend.Models;

namespace phonezone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;
        private static List<Product> cachedShockProducts = null;
        private static List<Product> cachedDealProducts = null;
        private static DateTime? shockCacheTime = null;
        private static DateTime? dealCacheTime = null;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

        public ProductsController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products.ToArray();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: api/Products/shock
        [HttpGet("shock")]
        public async Task<ActionResult<IEnumerable<Product>>> GetShockProducts()
        {
            if (cachedShockProducts == null || shockCacheTime == null || (DateTime.Now - shockCacheTime.Value) > CacheDuration)
            {
                cachedShockProducts = await _context.Products
                                                    .FromSqlRaw("SELECT TOP 10 * FROM Products ORDER BY NEWID()")
                                                    .ToListAsync();
                shockCacheTime = DateTime.Now; // Lưu thời gian cache
            }

            return Ok(cachedShockProducts.ToArray());
        }

        // GET: api/Products/deal
        [HttpGet("deal")]
        public async Task<ActionResult<IEnumerable<Product>>> GetDealProducts()
        {
            if (cachedDealProducts == null || dealCacheTime == null || (DateTime.Now - dealCacheTime.Value) > CacheDuration)
            {
                cachedDealProducts = await _context.Products
                                                   .FromSqlRaw("SELECT TOP 10 * FROM Products ORDER BY NEWID()")
                                                   .ToListAsync();
                dealCacheTime = DateTime.Now;
            }

            return Ok(cachedDealProducts.ToArray());
        }

        // GET: api/Products/dienthoai/{branch}
        [HttpGet("dienthoai/{branch}")]
        public async Task<ActionResult<Product>> GetProductByBranch(string branch)
        {
            var product = _context.Products.Where(p => p.Branch == branch).ToList();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.ToArray());
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}