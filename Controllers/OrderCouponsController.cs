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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderCouponsController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public OrderCouponsController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/OrderCoupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderCoupon>>> GetOrdersCoupons()
        {
            return await _context.OrdersCoupons.ToListAsync();
        }

        // GET: api/OrderCoupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderCoupon>> GetOrderCoupon(int id)
        {
            var orderCoupon = await _context.OrdersCoupons.FindAsync(id);

            if (orderCoupon == null)
            {
                return NotFound();
            }

            return orderCoupon;
        }

        // PUT: api/OrderCoupons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderCoupon(int id, OrderCoupon orderCoupon)
        {
            if (id != orderCoupon.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderCoupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderCouponExists(id))
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

        // POST: api/OrderCoupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderCoupon>> PostOrderCoupon(OrderCoupon orderCoupon)
        {
            _context.OrdersCoupons.Add(orderCoupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderCoupon", new { id = orderCoupon.Id }, orderCoupon);
        }

        // DELETE: api/OrderCoupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderCoupon(int id)
        {
            var orderCoupon = await _context.OrdersCoupons.FindAsync(id);
            if (orderCoupon == null)
            {
                return NotFound();
            }

            _context.OrdersCoupons.Remove(orderCoupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderCouponExists(int id)
        {
            return _context.OrdersCoupons.Any(e => e.Id == id);
        }
    }
}
