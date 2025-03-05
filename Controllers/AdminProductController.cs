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
    [Route("api/admin/[controller]")]
    [ApiController]
    public class AdminProductController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public AdminProductController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/AdminProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/AdminProduct/5
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

        // PUT: api/AdminProduct/5
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

        // POST: api/AdminProduct
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/AdminProduct/5
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

        // GET: api/admin/AdminProduct/total
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalProducts()
        {
            // Lấy tổng số lượng sản phẩm
            int totalProducts = await _context.Products.CountAsync();

            // Trả về tổng số lượng sản phẩm
            return Ok(totalProducts);
        }

        // GET: api/admin/AdminProduct/brand-product-count
        [HttpGet("brand-product-count")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductCountByBrand()
        {
            // Kiểm tra nếu bảng Brand hoặc Product không có dữ liệu
            if (_context.Brands == null || _context.Products == null)
            {
                return NotFound("Không tìm thấy dữ liệu về sản phẩm hoặc hãng.");
            }

            var productCountByBrand = await _context.Products
                .GroupBy(p => p.Branch) // Nhóm theo tên hãng (Branch)
                .Select(group => new
                {
                    BrandName = group.Key,       // Tên hãng
                    ProductCount = group.Count() // Số lượng sản phẩm trong nhóm đó
                })
                .ToListAsync();

            return Ok(productCountByBrand);
        }
    }
}
