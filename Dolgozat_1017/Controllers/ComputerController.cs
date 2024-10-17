using Dolgozat_1017.Context;
using Dolgozat_1017.DTO;
using Dolgozat_1017.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dolgozat_1017.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ComputerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComputerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Computer
        [HttpPost]
        public async Task<IActionResult> CreateComputer(CreateComputerDTO dto)
        {
            var room = await _context.Rooms.FindAsync(dto.RoomId);

            if (room == null)
                return NotFound(new { Message = "Room not found!" });

            var computer = new Computer { Model = dto.Model, RoomId = dto.RoomId };
            _context.Computers.Add(computer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComputer), new { id = computer.Id }, new ComputerDTO { Id = computer.Id, Model = computer.Model, RoomId = computer.RoomId });
        }

        // GET: api/Computer
        [HttpGet]
        public async Task<IActionResult> GetComputers()
        {
            var computers = await _context.Computers.Select(c => new ComputerDTO { Id = c.Id, Model = c.Model, RoomId = c.RoomId }).ToListAsync();
            return Ok(computers);
        }

        // GET: api/Computer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComputer(int id)
        {
            var computer = await _context.Computers.FindAsync(id);

            if (computer == null)
                return NotFound(new { Message = "Computer not found!" });

            return Ok(new ComputerDTO { Id = computer.Id, Model = computer.Model, RoomId = computer.RoomId });
        }

        // PUT: api/Computer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComputer(int id, UpdateComputerDTO dto)
        {
            var computer = await _context.Computers.FindAsync(id);

            if (computer == null)
                return NotFound(new { Message = "Computer not found!" });

            computer.Model = dto.Model;
            computer.RoomId = dto.RoomId;
            await _context.SaveChangesAsync();

            return Ok(new ComputerDTO { Id = computer.Id, Model = computer.Model, RoomId = computer.RoomId });
        }

        // DELETE: api/Computer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputer(int id)
        {
            var computer = await _context.Computers.FindAsync(id);

            if (computer == null)
                return NotFound(new { Message = "Computer not found!" });

            _context.Computers.Remove(computer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
