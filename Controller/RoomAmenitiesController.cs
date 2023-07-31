using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2_AysncInn.Data;
using Lab2_AysncInn.Models;

namespace Lab2_AysncInn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomAmenitiesController : ControllerBase
    {
        private readonly AsyncInnContext _context;

        public RoomAmenitiesController(AsyncInnContext context)
        {
            _context = context;
        }

        // GET: api/RoomAmenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomAmenity>>> GetRoomAmenity()
        {
          if (_context.RoomAmenity == null)
          {
              return NotFound();
          }
            return await _context.RoomAmenity.ToListAsync();
        }

        // GET: api/RoomAmenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomAmenity>> GetRoomAmenity(int id)
        {
          if (_context.RoomAmenity == null)
          {
              return NotFound();
          }
            var roomAmenity = await _context.RoomAmenity.FindAsync(id);

            if (roomAmenity == null)
            {
                return NotFound();
            }

            return roomAmenity;
        }

        // PUT: api/RoomAmenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomAmenity(int id, RoomAmenity roomAmenity)
        {
            if (id != roomAmenity.ID)
            {
                return BadRequest();
            }

            _context.Entry(roomAmenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomAmenityExists(id))
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

        // POST: api/RoomAmenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomAmenity>> PostRoomAmenity(RoomAmenity roomAmenity)
        {
          if (_context.RoomAmenity == null)
          {
              return Problem("Entity set 'AsyncInnContext.RoomAmenity'  is null.");
          }
            _context.RoomAmenity.Add(roomAmenity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomAmenity", new { id = roomAmenity.ID }, roomAmenity);
        }

        // DELETE: api/RoomAmenities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomAmenity(int id)
        {
            if (_context.RoomAmenity == null)
            {
                return NotFound();
            }
            var roomAmenity = await _context.RoomAmenity.FindAsync(id);
            if (roomAmenity == null)
            {
                return NotFound();
            }

            _context.RoomAmenity.Remove(roomAmenity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomAmenityExists(int id)
        {
            return (_context.RoomAmenity?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
