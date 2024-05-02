using Cine;
using CineMaster.Services;
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

        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
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

        [HttpGet]
        public async Task<IActionResult> ObtenerReservasPeliculasTerror([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                var reservas = await _reservaService.ObtenerReservasPeliculasTerror(fechaInicio, fechaFin);
                return Ok(reservas);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error al obtener las reservas.");
            }
        }


    }
}
