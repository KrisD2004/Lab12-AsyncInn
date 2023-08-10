﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2_AysncInn.Data;
using Lab2_AysncInn.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Lab2_AysncInn.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly AsyncInnContext _context;

        public RoomsController(AsyncInnContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoom()
        {
            if (_context.Room == null)
            {
                return NotFound();
            }

            //return await _context.Room.ToListAsync();
            return await _context.Room
                .Include(room => room.hotelRooms)
                .ThenInclude(hotelroom => hotelroom.Hotel)
                .Include(ro => ro.RoomAmenities)
                .ThenInclude(r => r.Amenity).ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            if (_context.Room == null)
            {
                return NotFound();
            }
            var room = await _context.Room.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.ID)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            if (_context.Room == null)
            {
                return Problem("Entity set 'AsyncInnContext.Room'  is null.");
            }
            _context.Room.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.ID }, room);
        }

        [HttpPost]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> PostAmenityToRoom(int amenityID, int roomID)
        {
            if (_context.RoomAmenity == null)
            {
                return Problem("Enity set 'AsyncInnContext.Amenity'  is null.");
            }

            var amenity = _context.Amenity.FindAsync(amenityID);
            if (amenity == null)
            {
                return Problem("No amenity with that ID Exists");
            }
            var room = _context.Room.FindAsync(roomID);
            if (room == null)
            {
                return Problem("No room with that ID exits");

            }
            RoomAmenity newRA = new RoomAmenity();
            try
            {
              newRA=  _context.RoomAmenities.Add(new RoomAmenity { AmenityID = amenityID, RoomID = roomID }).Entity;
            }

            catch (Exception ex)
            {

            } 
            finally
            {
              await   _context.SaveChangesAsync();
            }
            return CreatedAtAction("PostAmenityToRoom", newRA.ID, newRA);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteRoom(int id)
        {
            if (_context.Room == null)
            {
                return NotFound();
            }
            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{roomId}/Amenity/{amenityId}")]
        public async Task<IActionResult> DeleteRoom(int amenityID, int roomID)
        {
            if (_context.RoomAmenity == null)
            {
                return Problem("Enity set 'AsyncInnContext.Amenity'  is null.");
            }

            var amenity = _context.Amenity.FindAsync(amenityID);
            if (amenity == null)
            {
                return Problem("No amenity with that ID Exists");
            }
            var room = _context.Room.FindAsync(roomID);
            if (room == null)
            {
                return Problem("No room with that ID exits");

            }
            
            try
            {
                RoomAmenity oldRA = await _context.RoomAmenity.FindAsync(new { AmenityID = amenityID, RoomID = roomID });
                _context.RoomAmenities.Remove(oldRA);
            }

            catch (Exception ex)
            {

            }
            finally
            {
                await _context.SaveChangesAsync();
            }
            return Ok();

        }

        private bool RoomExists(int id)
        {
            return (_context.Room?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
