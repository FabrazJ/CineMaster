using Cine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CineMaster
{
    public class Reservas : DbContext
    {
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<BillboardEntity> Billboards { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }

        public IQueryable<BookingEntity> GetBookingsForHorrorMoviesWithinDateRange(DateTime startDate, DateTime endDate)
        {
            return Bookings
                .Where(b => b.Billboard.Movie.Genre == MovieGenreEnum.HORROR &&
                            b.Billboard.Date >= startDate && b.Billboard.Date <= endDate);
        }
    }
}
