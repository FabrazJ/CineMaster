using Cine;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CineMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly CineContext _context;

        public ReservasController(CineContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBookingsForHorrorMoviesWithinDateRange(DateTime startDate, DateTime endDate)
        {
            var bookings = _context.Bookings
                .Where(b => b.Billboard.Movie.Genre == MovieGenreEnum.HORROR &&
                            b.Billboard.Date >= startDate && b.Billboard.Date <= endDate)
                .ToList();
            return Ok(bookings);
        }
    }
}
