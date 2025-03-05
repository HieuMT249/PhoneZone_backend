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
    public class SpecificationsController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public SpecificationsController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/Specifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specification>>> GetSpecifications()
        {
            return await _context.Specifications.ToListAsync();
        }

        // GET: api/Specifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specification>> GetSpecification(int id)
        {
            var specification = await _context.Specifications.Where(s => s.ProductId == id).FirstAsync();

            if (specification == null)
            {
                Console.WriteLine($"Không tìm thấy sản phẩm với id {id}");
                return NotFound(new { message = "Không tìm thấy sản phẩm" });
            }

            return Ok(specification);

        }



        // PUT: api/Specifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecification(int id, Specification specification)
        {
            if (id != specification.Id)
            {
                return BadRequest();
            }

            _context.Entry(specification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecificationExists(id))
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

        // POST: api/Specifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specification>> PostSpecification(Specification specification)
        {
            _context.Specifications.Add(specification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecification", new { id = specification.Id }, specification);
        }

        // DELETE: api/Specifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecification(int id)
        {
            var specification = await _context.Specifications.FindAsync(id);
            if (specification == null)
            {
                return NotFound();
            }

            _context.Specifications.Remove(specification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecificationExists(int id)
        {
            return _context.Specifications.Any(e => e.Id == id);
        }
    }
}
