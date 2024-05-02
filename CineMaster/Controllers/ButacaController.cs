using Microsoft.AspNetCore.Mvc;
using Cine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace CineMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ButacaController : Controller
    {
        private readonly CineContext _context;

        public ButacaController(CineContext context)
        {
            _context = context;
        }

        // Obtener todas las butacas
        [HttpGet]
        public async Task<IActionResult> GetAllSeats()
        {
            var seats = await _context.Seats.ToListAsync();
            return Ok(seats);
        }

        // Obtener una butaca por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeatById(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            return Ok(seat);
        }

        // Crear una nueva butaca
        [HttpPost]
        public async Task<IActionResult> CreateSeat(SeatEntity seat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeatById), new { id = seat.Id }, seat);
        }

        // Actualizar una butaca existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeat(int id, SeatEntity seat)
        {
            if (id != seat.Id)
            {
                return BadRequest();
            }

            _context.Entry(seat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeatExists(id))
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

        // Eliminar una butaca
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeat(int id)
        {
            var seat = await _context.Seats.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }

            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeatExists(int id)
        {
            return _context.Seats.Any(e => e.Id == id);
        }
    }
}
