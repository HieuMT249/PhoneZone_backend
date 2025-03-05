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
using phonezone_backend.Services.VNPay;

namespace phonezone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;
        private readonly IVnPayService _vnPayService;

        public CartsController(PhoneZoneDBContext context)
        {
            _context = context;
        }


        [HttpGet("count/{userId}")]
        public async Task<IActionResult> GetCartCount(int userId)
        {
            var cartItemsCount = await _context.CartItems
                                               .Where(ci => ci.Cart.UserId == userId)
                                               .CountAsync();
            return Ok(new { count = cartItemsCount });
        }

        [HttpGet("items/{userId}")]
        public async Task<IActionResult> GetCartItem(int userId)
        {
            var cartItems = await _context.CartItems
                                               .Where(ci => ci.Cart.UserId == userId)
                                               .ToListAsync();
            return Ok(cartItems.ToArray());
        }

        // POST: api/Carts/AddItem
        [HttpPost("add-cart")]
        public async Task<ActionResult<CartItem>> AddItemToCart(AddCartRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng");
            }


            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == request.UserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = request.UserId
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == request.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                _context.Entry(existingCartItem).State = EntityState.Modified;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
