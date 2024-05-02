using Cine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace CineMaster
{
    public class Sala
    {
        private readonly CineContext _context;

        public Sala(CineContext context)
        {
            _context = context;
        }

        public IQueryable<object> GetSeatsAvailabilityForToday()
        {
            // Obtener la fecha actual
            DateTime currentDate = DateTime.Today;

            // Consulta para obtener el número de butacas ocupadas y disponibles por sala
            var seatsAvailability = _context.Rooms
                .Include(r => r.Billboards)
                    .ThenInclude(b => b.Seats)
                .Select(r => new
                {
                    RoomName = r.Name,
                    TotalSeats = r.Billboards.SelectMany(b => b.Seats).Count(),
                    OccupiedSeats = r.Billboards.SelectMany(b => b.Seats).Count(s => s.Bookings.Any(b => b.Billboard.Date == currentDate)),
                    AvailableSeats = r.Billboards.SelectMany(b => b.Seats).Count() - r.Billboards.SelectMany(b => b.Seats).Count(s => s.Bookings.Any(b => b.Billboard.Date == currentDate))
                });

            return seatsAvailability;
        }
    }
}
