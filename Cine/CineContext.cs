using Microsoft.EntityFrameworkCore;
namespace Cine
{
    public class CineContext : DbContext
    {
      public CineContext(DbContextOptions<CineContext> options)
        :base(options)
        {
        
        }

        public object Bookings { get; set; }
        public object Billboards { get; set; }
        public object Movies { get; set; }
    }
}