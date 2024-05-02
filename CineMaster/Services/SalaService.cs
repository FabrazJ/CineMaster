using Cine;
using CineMaster.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic; 


namespace CineMaster.Services
{
    public class SalaService : ISalaService
    {
        private readonly CineContext _context;

        public SalaService(CineContext context)
        {
            _context = context;
        }
public async Task<List<CancellationResultDto>> CancelarCarteleraYReservas(DTOBillboardsCancellation cancellationDto)
{
    using var transaction = _context.Database.BeginTransaction();

    try
    {
         var billboard = await _context.Billboards
              .Include(b => b.Bookings) // Carga las reservas junto con la cartelera
              .FirstOrDefaultAsync(b => b.Id == cancellationDto.BillboardId);


                if (billboard != null)
        {
            // Lista para almacenar los clientes afectados
            List<CancellationResultDto> affectedClients = new List<CancellationResultDto>();

                    // Verificar si la fecha de la cartelera es anterior a la fecha actual
                    if (billboard.Date < DateTime.Now)
                    {
                        throw new InvalidOperationException("No se puede cancelar funciones de la cartelera con fecha anterior a la actual");
                    }

                    // Cancelar reservas asociadas
                    foreach (var booking in billboard.Bookings)
            {
                // Habilitar la butaca correspondiente
                var seat = await _context.Seats.FirstOrDefaultAsync(s => s.Id == booking.SeatId);
                seat.Status = true;

                // Agregar cliente afectado a la lista
                affectedClients.Add(new CancellationResultDto
                {
                    ClientId = booking.CustomerId,
                    ClientName = $"{booking.Customer.Name} {booking.Customer.Lastname}"
                });

                // Eliminar la reserva
                _context.Bookings.Remove(booking);
            }

            // Eliminar la cartelera
            _context.Billboards.Remove(billboard);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Commit transaction si todo está bien
            await transaction.CommitAsync();

            // Imprimir la lista de clientes afectados por consola
            foreach (CancellationResultDto client in affectedClients)
            {
                Console.WriteLine($"Cliente ID: {client.ClientId}, Nombre: {client.ClientName}");
            }

            return affectedClients;
        }
        else
        {
            // Rollback transaction si la cartelera no existe
            await transaction.RollbackAsync();
            return null;
        }
    }
    catch (Exception ex)
    {
        // Rollback transaction si hay alguna excepción
        await transaction.RollbackAsync();
        throw ex;
    }

            throw new NotImplementedException();
}
        public async Task<bool> InhabilitarButacaYCancelarReserva(DTOBooking bookingDto, SeatDto seatDto)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                // Obtener la reserva y la butaca
                var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingDto.BookingId);
                var seat = await _context.Seats.FirstOrDefaultAsync(s => s.Id == seatDto.SeatId);

                if (booking != null && seat != null)
                {
                    // Inhabilitar la butaca
                    seat.Status = false;

                    // Eliminar la reserva
                    _context.Bookings.Remove(booking);

                    // Guardar los cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // Commit transaction si todo está bien
                    await transaction.CommitAsync();

                    return true;
                }
                else
                {
                    // Rollback transaction si hay algún problema
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            catch (Exception)
            {
                // Rollback transaction si hay alguna excepción
                await transaction.RollbackAsync();
                throw;
            }
        }

    }


}
