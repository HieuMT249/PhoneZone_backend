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
    public class AdminUserController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public AdminUserController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/AdminUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/AdminUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/AdminUser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest("ID người dùng không hợp lệ.");
            }

            // Kiểm tra email đã tồn tại chưa, nhưng không tính chính user đang cập nhật
            var existingEmailUser = await _context.Users
                .Where(u => u.Email == user.Email && u.Id != id)
                .FirstOrDefaultAsync();

            if (existingEmailUser != null)
            {
                return BadRequest("Email đã được sử dụng bởi người dùng khác.");
            }

            // Kiểm tra số điện thoại đã tồn tại chưa, nhưng không tính chính user đang cập nhật
            var existingPhoneUser = await _context.Users
                .Where(u => u.PhoneNumber == user.PhoneNumber && u.Id != id)
                .FirstOrDefaultAsync();

            if (existingPhoneUser != null)
            {
                return BadRequest("Số điện thoại đã được sử dụng bởi người dùng khác.");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.Id == id))
                {
                    return NotFound("Người dùng không tồn tại.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Cập nhật thông tin thành công.");
        }

        // POST: api/AdminUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/AdminUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // GET: api/admin/AdminUser/total
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotalUsers()
        {
            // Lấy tổng số người dùng
            int totalUsers = await _context.Users.Where(u => u.Role.ToLower() != "admin").CountAsync();

            // Trả về tổng số lượng người dùng
            return Ok(totalUsers);
        }

        // GET: api/admin/AdminUser/without-admin
        [HttpGet("without-admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersWithoutAdmin()
        {
            // Lọc ra những người dùng không có vai trò là "Admin"
            var usersWithoutAdmin = await _context.Users
                                                  .Where(u => u.Role.ToLower() != "admin")
                                                  .ToListAsync();

            return Ok(usersWithoutAdmin);
        }

        // PATCH: api/admin/AdminUser/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateUserStatus(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Đảo ngược trạng thái hoạt động
            user.isActive = !user.isActive;

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cập nhật trạng thái thành công", isActive = user.isActive });
        }
    }
}
