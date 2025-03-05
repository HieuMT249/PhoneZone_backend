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
    public class AdminSpecificationController : ControllerBase
    {
        private readonly PhoneZoneDBContext _context;

        public AdminSpecificationController(PhoneZoneDBContext context)
        {
            _context = context;
        }

        // GET: api/AdminSpecification
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specification>>> GetSpecifications()
        {
            return await _context.Specifications.ToListAsync();
        }

        // GET: api/AdminSpecification/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specification>> GetSpecification(int id)
        {
            var specification = await _context.Specifications.FindAsync(id);

            if (specification == null)
            {
                return NotFound();
            }

            return specification;
        }

        // PUT: api/AdminSpecification/5
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

        // POST: api/AdminSpecification
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specification>> PostSpecification(Specification specification)
        {
            _context.Specifications.Add(specification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecification", new { id = specification.Id }, specification);
        }

        // DELETE: api/AdminSpecification/5
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
        // GET: api/AdminSpecification/product/5
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<Specification>> GetSpecificationByProductId(int productId)
        {
            var specification = await _context.Specifications
                                              .FirstOrDefaultAsync(s => s.ProductId == productId);

            if (specification == null)
            {
                return NotFound();
            }

            return Ok(specification);
        }
        // PUT: api/admin/AdminSpecification/product/5
        [HttpPut("product/{productId}")]
        public async Task<IActionResult> UpdateSpecificationByProductId(int productId, [FromBody] Specification updatedSpecification)
        {
            // Tìm Specification theo productId
            var existingSpecification = await _context.Specifications
                                                      .FirstOrDefaultAsync(s => s.ProductId == productId);

            if (existingSpecification == null)
            {
                return NotFound("Specification not found for the given productId.");
            }

            // Cập nhật các trường thông tin
            existingSpecification.Thumbnails = updatedSpecification.Thumbnails;
            existingSpecification.OutstandingFeatures = updatedSpecification.OutstandingFeatures;
            existingSpecification.Camera = updatedSpecification.Camera;
            existingSpecification.Model = updatedSpecification.Model;
            existingSpecification.Colour = updatedSpecification.Colour;
            existingSpecification.Weight = updatedSpecification.Weight;
            existingSpecification.Video = updatedSpecification.Video;
            existingSpecification.CameraTrueDepth = updatedSpecification.CameraTrueDepth;
            existingSpecification.ChargingConnectivity = updatedSpecification.ChargingConnectivity;
            existingSpecification.Battery = updatedSpecification.Battery;
            existingSpecification.Country = updatedSpecification.Country;
            existingSpecification.Company = updatedSpecification.Company;
            existingSpecification.Guarantee = updatedSpecification.Guarantee;
            existingSpecification.WaterResistant = updatedSpecification.WaterResistant;
            existingSpecification.CameraFeatures = updatedSpecification.CameraFeatures;
            existingSpecification.GPU = updatedSpecification.GPU;
            existingSpecification.Pin = updatedSpecification.Pin;
            existingSpecification.ChargingSupport = updatedSpecification.ChargingSupport;
            existingSpecification.NetworkSupport = updatedSpecification.NetworkSupport;
            existingSpecification.WiFi = updatedSpecification.WiFi;
            existingSpecification.Bluetooth = updatedSpecification.Bluetooth;
            existingSpecification.GPS = updatedSpecification.GPS;
            existingSpecification.ChargingTechnology = updatedSpecification.ChargingTechnology;
            existingSpecification.FingerprintSensor = updatedSpecification.FingerprintSensor;
            existingSpecification.SpecialFeatures = updatedSpecification.SpecialFeatures;
            existingSpecification.RearCamera = updatedSpecification.RearCamera;
            existingSpecification.FrontCamera = updatedSpecification.FrontCamera;
            existingSpecification.SIM = updatedSpecification.SIM;
            existingSpecification.Sensor = updatedSpecification.Sensor;
            existingSpecification.Ram = updatedSpecification.Ram;
            existingSpecification.CPU = updatedSpecification.CPU;
            existingSpecification.NFC = updatedSpecification.NFC;
            existingSpecification.Chip = updatedSpecification.Chip;
            existingSpecification.ScreenResolution = updatedSpecification.ScreenResolution;
            existingSpecification.ScreenFeatures = updatedSpecification.ScreenFeatures;
            existingSpecification.InternalMemory = updatedSpecification.InternalMemory;
            existingSpecification.BatteryCapacity = updatedSpecification.BatteryCapacity;
            existingSpecification.ScreenSize = updatedSpecification.ScreenSize;
            existingSpecification.Screen = updatedSpecification.Screen;
            existingSpecification.OperatingSystem = updatedSpecification.OperatingSystem;

            // Lưu thay đổi vào database
            _context.Entry(existingSpecification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating specification.");
            }

            return NoContent(); // Trả về 204 nếu thành công
        }

    }
}
