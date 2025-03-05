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
    [Route("api/[controller]")]
    [ApiController]
    public class WishListItemsController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public WishListItemsController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/WishListItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishListItem>>> GetWishListItems()
        {
            return await _context.WishListItems.ToListAsync();
        }

        // GET: api/WishListItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishListItem>> GetWishListItem(int id)
        {
            var wishListItem = await _context.WishListItems.FindAsync(id);

            if (wishListItem == null)
            {
                return NotFound();
            }

            return wishListItem;
        }

        // PUT: api/WishListItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishListItem(int id, WishListItem wishListItem)
        {
            if (id != wishListItem.WishListId)
            {
                return BadRequest();
            }

            _context.Entry(wishListItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishListItemExists(id))
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

        // POST: api/WishListItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost()]
        public async Task<ActionResult<WishListItem>> PostWishListItem(WishListItem wishListItem)
        {
            _context.WishListItems.Add(wishListItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get WishList Item", new { id = wishListItem.Id }, wishListItem);
        }

        // DELETE: api/WishListItems/{userId}/{id}
        [HttpDelete("{userId}/{produtcId}")]
        public async Task<IActionResult> DeleteWishListItem(int userId, int produtcId)
        {
            var wishListItem = await _context.WishListItems.Where(item => item.WishList.UserId == userId)
                .FirstOrDefaultAsync(item => item.ProductId == produtcId);

            if (wishListItem == null)
            {
                return NotFound();
            }

            _context.WishListItems.Remove(wishListItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/WishListItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishListItem(int id)
        {
            var wishListItem = await _context.WishListItems.FindAsync(id);
            if (wishListItem == null)
            {
                return NotFound();
            }

            _context.WishListItems.Remove(wishListItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WishListItemExists(int id)
        {
            return _context.WishListItems.Any(e => e.WishListId == id);
        }
    }
}