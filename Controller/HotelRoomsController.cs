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
    public class HotelRoomsController : ControllerBase
    {
        private readonly AsyncInnContext _context;

        public HotelRoomsController(AsyncInnContext context)
        {
            _context = context;
        }

        // GET: api/HotelRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRoom()
        {
          if (_context.HotelRoom == null)
          {
              return NotFound();
          }
            return await _context.HotelRoom.ToListAsync();
        }


        //new route to get all the rooms for a hotelIDX
        //new route for lab 14
        [HttpGet]
        [Route("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetAllRoomsForHotelAsync(int hotelId)
        {
            if (hotelId == 0)
            {
                return NotFound();
            }
            var hotelRoom = await _context.HotelRoom.Where(hr => hr.HotelID == hotelId).ToListAsync();

            return hotelRoom;    
          
        }

        //new route to find specifc room
        // route added for lab 14
        [HttpGet]
        [Route("/api/Hotels/{hotelId}/Rooms/{roomID}")]
        public async Task<ActionResult<HotelRoom>> GetAllRoomDetails(int hotelID, int roomID)
        {
            if (hotelID == 0 || roomID == 0)
            {
                return NotFound();
            }
            var specificRoom = await _context.HotelRoom.Where(r => r.HotelID == hotelID && r.RoomID == roomID)
                .Include(hr=> hr.Room)
                .FirstOrDefaultAsync();

            return specificRoom;
        }

        // GET: api/HotelRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int id)
        {
          if (_context.HotelRoom == null)
          {
              return NotFound();
          }
            var hotelRoom = await _context.HotelRoom.FindAsync(id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return hotelRoom;
        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRoom(int id, HotelRoom hotelRoom)
        {
            if (id != hotelRoom.ID)
            {
                return BadRequest();
            }

            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelRoomExists(id))
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

        //new put route for lab14
        //PUT update the details of a specific room:
        [HttpPut]
        [Route("/api/Hotels/{hotelId}/Rooms/{roomID}")]
        public async Task<ActionResult<HotelRoom>> PutUpdate(int hotelId, [FromQuery] int roomId, [FromBody] HotelRoom hotelRoom)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);
            var room = await _context.Rooms.FindAsync(roomId);

            if (hotel == null)
            {
                return NotFound($"Hotel with ID {hotelId} not found.");
            }
            else if (room == null)
            {
                return NotFound();
            }

            //hotelRoom.HotelID = hotelId;

            HotelRoom NewHotelRoom = new HotelRoom() { HotelID = hotel.ID, RoomID = room.ID, Name = hotelRoom.Name, Price= hotelRoom.Price };
            _context.HotelRooms.Add(NewHotelRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("AddRoomToHotel", new { id = NewHotelRoom.ID }, NewHotelRoom);

        }



        // POST: api/HotelRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
          if (_context.HotelRoom == null)
          {
              return Problem("Entity set 'AsyncInnContext.HotelRoom'  is null.");
          }
            _context.HotelRoom.Add(hotelRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.ID }, hotelRoom);
        }


        //new post route to add a room to a hotel
        //route added for the lab 14
        [HttpPost]
        [Route("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoom>> AddRoomToHotel(int hotelId, [FromQuery] int roomId, [FromBody] HotelRoom hotelRoom)
        {
            var hotel = await _context.Hotels.FindAsync(hotelId);
            var room = await _context.Rooms.FindAsync(roomId);

            if (hotel == null)
            {
                return NotFound($"Hotel with ID {hotelId} not found.");
            }
            else if (room == null)
            {
                return NotFound();
            }


            HotelRoom NewHotelRoom = new HotelRoom() { HotelID = hotel.ID, RoomID = room.ID, Name = hotelRoom.Name, Price= hotelRoom.Price };
            _context.HotelRooms.Add(NewHotelRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("AddRoomToHotel", new { id = NewHotelRoom.ID }, NewHotelRoom);

        }


       
        // DELETE: api/HotelRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelRoom(int id)
        {
            if (_context.HotelRoom == null)
            {
                return NotFound();
            }
            var hotelRoom = await _context.HotelRoom.FindAsync(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            _context.HotelRoom.Remove(hotelRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelRoomExists(int id)
        {
            return (_context.HotelRoom?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
