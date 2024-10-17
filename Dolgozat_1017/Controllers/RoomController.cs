using Dolgozat_1017.Context;
using Dolgozat_1017.DTO;
using Dolgozat_1017.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Dolgozat_1017.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Room
        [HttpPost]
        public async Task<IActionResult> CreateRoom(CreateRoomDTO dto)
        {
            //var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            //return Ok(claims); // Temporary debug to inspect the claims


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null || !_context.Users.Any(u => u.Id == userId))
            {
                return BadRequest("User not found.");
            }

            var room = new Room { Name = dto.Name, Capacity = dto.Capacity, UserId = userId };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, new RoomDTO { Id = room.Id, Name = room.Name, Capacity = room.Capacity });
        }

        // GET: api/Room
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _context.Rooms.Select(r => new RoomDTO { Id = r.Id, Name = r.Name }).ToListAsync();
            return Ok(rooms);
        }

        // GET: api/Room/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
                return NotFound(new { Message = "Room not found!" });

            return Ok(new RoomDTO { Id = room.Id, Name = room.Name });
        }

        // PUT: api/Room/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, UpdateRoomDTO dto)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
                return NotFound(new { Message = "Room not found!" });

            room.Name = dto.Name;
            await _context.SaveChangesAsync();

            return Ok(new RoomDTO { Id = room.Id, Name = room.Name });
        }

        // DELETE: api/Room/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
                return NotFound(new { Message = "Room not found!" });

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
