using Cine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalaController : ControllerBase
    {
        private readonly CineContext _context;

        public SalaController(CineContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSeatsAvailabilityForToday()
        {
            // Obtener la fecha actual
            DateTime currentDate = DateTime.Today;

            // Consulta para obtener el número de butacas ocupadas y disponibles por sala
            var seatsAvailability = _context.Rooms
                .Include(r => r.Billboards)
                .Where(r => r.Billboards.Any(b => b.Date == currentDate))
                .Select(r => new
                {
                    RoomName = r.Name,
                    TotalSeats = r.Billboards.SelectMany(b => b.Seats).Count(),
                    OccupiedSeats = r.Billboards.SelectMany(b => b.Seats).Count(s => s.Bookings.Any(b => b.Billboard.Date == currentDate)),
                    AvailableSeats = r.Billboards.SelectMany(b => b.Seats).Count() - r.Billboards.SelectMany(b => b.Seats).Count(s => s.Bookings.Any(b => b.Billboard.Date == currentDate))
                })
                .ToList();

            return Ok(seatsAvailability);
        }
    }
}
