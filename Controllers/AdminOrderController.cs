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
    public class AdminOrderController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public AdminOrderController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/AdminOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/AdminOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/AdminOrder/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/AdminOrder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/AdminOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        // GET: api/admin/AdminOrder/total
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalOrders()
        {
            // Lấy tổng số đơn hàng
            int totalOrders = await _context.Orders.CountAsync();

            // Trả về tổng số lượng đơn hàng
            return Ok(totalOrders);
        }

        // GET: api/admin/AdminOrder/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUser(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound("Không tìm thấy đơn hàng cho người dùng này.");
            }

            return Ok(orders);
        }
        // GET: api/admin/AdminOrder/statistics
        [HttpGet("statistics")]
        public async Task<ActionResult<IEnumerable<object>>> GetMonthlyStatistics([FromQuery] int month, [FromQuery] int year)
        {
            if (month < 1 || month > 12 || year < 1)
            {
                return BadRequest("Tháng hoặc năm không hợp lệ.");
            }

            // Lọc đơn hàng theo tháng, năm và trạng thái "Đã giao"
            var orders = await _context.Orders
                .Where(o => o.CreatedDate.Month == month &&
                            o.CreatedDate.Year == year &&
                            o.Status == "Đã giao")
                .ToListAsync();

            // Chuyển đổi và nhóm theo ngày
            var groupedOrders = orders
                .GroupBy(o => o.CreatedDate.Date)
                .Select(group => new
                {
                    Date = group.Key.ToString("yyyy-MM-dd"),
                    Revenue = group.Sum(o =>
                    {
                        // Xóa dấu chấm (.) và chữ "đ" trước khi chuyển đổi
                        string cleanedAmount = o.FinalAmount.Replace(".", "").Replace("đ", "").Trim();

                        if (decimal.TryParse(cleanedAmount, out decimal amount))
                        {
                            return amount;
                        }
                        return 0;
                    })
                })
                .ToList();

            return Ok(groupedOrders);
        }

    }
}
