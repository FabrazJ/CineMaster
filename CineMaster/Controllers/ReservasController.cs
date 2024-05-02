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
            var bookings = _context.GetBookingsForHorrorMoviesWithinDateRange(startDate, endDate)
                .ToList();
            return Ok(bookings);
        }
    }
}
