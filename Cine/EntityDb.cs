using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine
{
    public class EntityDb : DbContext
    {
        public DbSet<BillboardEntity> Billboards { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<SeatEntity> Seats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("your_connection_string_here");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingEntity>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<BookingEntity>()
                .HasOne(b => b.Seat)
                .WithMany()
                .HasForeignKey(b => b.SeatId);

            modelBuilder.Entity<BookingEntity>()
                .HasOne(b => b.Billboard)
                .WithMany()
                .HasForeignKey(b => b.BillboardId);


            modelBuilder.Entity<BillboardEntity>()
                .HasOne(b => b.Movie)
                .WithMany()
                .HasForeignKey(b => b.MovieId);


            modelBuilder.Entity<BillboardEntity>()
                .HasOne(b => b.Room)
                .WithMany()
                .HasForeignKey(b => b.RoomId);

            modelBuilder.Entity<BillboardEntity>()
                .HasMany(b => b.Bookings) // Indica que una cartelera puede tener muchas reservas
                .WithOne(b => b.Billboard) // Indica que una reserva pertenece a una sola cartelera
                .HasForeignKey(b => b.BillboardId);

            modelBuilder.Entity<SeatEntity>()
                .HasOne(s => s.Room)
                .WithMany()
                .HasForeignKey(s => s.RoomId);

       

            modelBuilder.Entity<RoomEntity>()
                .HasMany(r => r.Billboards)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId);

            modelBuilder.Entity<SeatEntity>()
                .HasMany(s => s.Bookings)
                .WithOne(b => b.Seat)
                .HasForeignKey(b => b.SeatId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
