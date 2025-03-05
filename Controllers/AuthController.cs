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

namespace phonezone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(PhoneZoneDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

            if(!VerifyPassword(request.Password, user.Password))
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
    }
}
