using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using phonezone_backend.Data;
using phonezone_backend.Models;

namespace phonezone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListsController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public WishListsController(PhoneZoneDBContext context)
        {
            _context = context;
        }



        // GET: api/WishLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishList>>> GetWishList()
        {
            return await _context.WishList.ToListAsync();
        }

        // GET: api/WishLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishList>> GetWishList(int id)
        {
            var wishList = await _context.WishList.FindAsync(id);

            if (wishList == null)
            {
                return NotFound();
            }

            return wishList;
        }

        // PUT: api/WishLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishList(int id, WishList wishList)
        {
            if (id != wishList.Id)
            {
                return BadRequest();
            }

            _context.Entry(wishList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishListExists(id))
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

        [HttpGet("items/{userId}")]
        public async Task<IActionResult> GetWishlistItem(int userId)
        {
            var cartItems = await _context.WishListItems
                                               .Where(ci => ci.WishList.UserId == userId)
                                               .ToListAsync();
            return Ok(cartItems);
        }

        // POST: api/WishLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WishList>> PostWishList(WishList wish)
        {
            var user = await _context.Users.FindAsync(wish.UserId);
            if (user == null)
            {
                return NotFound("Không tìm thấy người dùng");
            }

            var wishlist = await _context.WishList.FirstOrDefaultAsync(c => c.UserId == wish.UserId);
            if (wishlist == null)
            {
                wishlist = new WishList
                {
                    UserId = wish.UserId
                };
                _context.WishList.Add(wishlist);
                await _context.SaveChangesAsync();
            }

            var existingWishlistItem = await _context.WishListItems.FirstOrDefaultAsync(ci => ci.WishListId == wishlist.Id && ci.ProductId == wish.Id);

            var wishlistItem = new WishListItem
            {
                WishListId = wishlist.Id,
                ProductId = wish.Id,
            };
            _context.WishListItems.Add(wishlistItem);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/WishLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishList(int id)
        {
            var wishList = await _context.WishList.FindAsync(id);
            if (wishList == null)
            {
                return NotFound();
            }

            _context.WishList.Remove(wishList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WishListExists(int id)
        {
            return _context.WishList.Any(e => e.Id == id);
        }
    }
}
