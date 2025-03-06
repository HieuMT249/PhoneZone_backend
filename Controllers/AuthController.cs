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
using BCrypt.Net;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using phonezone_backend.Services;
using System.Text;

namespace phonezone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public AuthController(PhoneZoneDBContext context, IConfiguration configuration, EmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if(user == null)
            {
                if (request.Password == request.ConfirmPassword)
                {
                    User newUser = new User
                    {
                        Email = request.Email,
                        Password = CreateHashPassword(request.Password),
                        Role = "User",
                        CreatedDate = DateTime.UtcNow,
                        Name = request.Name,
                        PhoneNumber = request.PhoneNumber,
                    };

                    _context.Users.Add(newUser);

                    await _context.SaveChangesAsync();

                    return Ok("Tạo tài khoản thành công.");
                }
            }
            
            return BadRequest("Email đã tồn tại trong hệ thống, vui lòng sử dụng email khác để tiếp tục.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return BadRequest("Không tìm thấy thông tin tài khoản");
            }

            if (!user.isActive) // Kiểm tra tài khoản có đang hoạt động không
            {
                return BadRequest("Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ quản trị viên.");
            }

            if (!VerifyPassword(request.Password, user.Password))
            {
                return BadRequest("Sai mật khẩu");
            }

            string token = CreateJwtToken(user);

            return Ok(token);
        }


        private string CreateJwtToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credential
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token); 

            return jwt;
        }

        private string CreateHashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string typePassword, string dbPassword)
        {
            return BCrypt.Net.BCrypt.Verify(typePassword, dbPassword);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return BadRequest(new { message = "Email không tồn tại" });

            // Tạo token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value); // Đọc khóa bí mật từ appsettings.json
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userId", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var resetLink = $"http://localhost:5173/reset-password/{tokenHandler.WriteToken(token)}";

            _emailService.SendResetPasswordEmail(user.Email, resetLink);
            return Ok(new { message = "Link đặt lại mật khẩu đã được gửi!" });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
                var claims = tokenHandler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var userId = int.Parse(claims.Claims.First(x => x.Type == "userId").Value);
                var user = await _context.Users.FindAsync(userId);
                if (user == null) return BadRequest(new { message = "User không tồn tại" });

                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Đặt lại mật khẩu thành công!" });
            }
            catch
            {
                return BadRequest(new { message = "Token không hợp lệ!" });
            }
        }
    }
}
