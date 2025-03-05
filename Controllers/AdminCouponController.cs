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
    public class AdminCouponController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public AdminCouponController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/AdminCoupon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetCoupons()
        {
            return await _context.Coupons.ToListAsync();
        }

        // GET: api/AdminCoupon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetCoupon(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);

            if (coupon == null)
            {
                return NotFound();
            }

            return coupon;
        }

        // PUT: api/AdminCoupon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoupon(int id, Coupon coupon)
        {
            if (id != coupon.Id)
            {
                return BadRequest();
            }

            // Kiểm tra xem giá trị có nhỏ hơn 100 không
            if (coupon.Value >= 100)
            {
                return BadRequest("Giá trị mã coupon phải nhỏ hơn 100.");
            }

            // Kiểm tra xem mã coupon đã tồn tại chưa, trừ chính nó
            var existingCoupon = await _context.Coupons
                .FirstOrDefaultAsync(c => c.Code == coupon.Code && c.Id != id);

            if (existingCoupon != null)
            {
                return BadRequest("Mã coupon này đã tồn tại.");
            }

            _context.Entry(coupon).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/AdminCoupon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coupon>> PostCoupon(Coupon coupon)
        {
            // Kiểm tra xem giá trị có nhỏ hơn 100 không
            if (coupon.Value >= 100)
            {
                return BadRequest("Giá trị mã coupon phải nhỏ hơn 100.");
            }

            // Kiểm tra xem mã coupon đã tồn tại chưa
            var existingCoupon = await _context.Coupons
                .FirstOrDefaultAsync(c => c.Code == coupon.Code);

            if (existingCoupon != null)
            {
                return BadRequest("Mã coupon này đã tồn tại.");
            }

            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoupon", new { id = coupon.Id }, coupon);
        }

        // DELETE: api/AdminCoupon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CouponExists(int id)
        {
            return _context.Coupons.Any(e => e.Id == id);
        }
        // GET: api/admin/AdminCoupon/total
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalCoupons()
        {
            // Lấy tổng số lượng coupon
            int totalCoupons = await _context.Coupons.CountAsync();

            // Trả về tổng số lượng coupon
            return Ok(totalCoupons);
        }
    }
}
