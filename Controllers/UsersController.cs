using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using phonezone_backend.Data;
using phonezone_backend.DTO.request;
using phonezone_backend.Models;

namespace phonezone_backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public UsersController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/email
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
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

        // GET: api/Users/email
        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email không hợp lệ.");
            }

            var user = await _context.Users
                                     .Where(u => u.Email.ToLower() == email.ToLower()) // So sánh không phân biệt hoa thường
                                     .FirstOrDefaultAsync(); // Tránh lỗi nếu không có kết quả

            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng.");
            }

            return Ok(user);
        }
        [HttpPut("changepassword/{id}")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordModel model)
        {
            if (string.IsNullOrWhiteSpace(model.OldPassword) ||
                string.IsNullOrWhiteSpace(model.NewPassword) )
            {
                return BadRequest("Vui lòng nhập đầy đủ thông tin.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Người dùng không tồn tại.");
            }

            // Kiểm tra mật khẩu cũ bằng BCrypt
            if (!BCrypt.Net.BCrypt.Verify(model.OldPassword, user.Password))
            {
                return BadRequest("Mật khẩu cũ không đúng.");
            }

            // Mã hóa mật khẩu mới và cập nhật
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Đổi mật khẩu thành công.");
        }

    }
}
