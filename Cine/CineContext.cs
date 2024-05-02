using Microsoft.EntityFrameworkCore;

namespace Cine
{
    public class CineContext : DbContext
    {
        public CineContext(DbContextOptions<CineContext> options)
            : base(options)
        {

        }
    
        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<SeatEntity> Seats { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<BillboardEntity> Billboards { get;  set; }
        public DbSet<MovieEntity> Movies { get; set; }

        public object GetBookingsForHorrorMoviesWithinDateRange(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
