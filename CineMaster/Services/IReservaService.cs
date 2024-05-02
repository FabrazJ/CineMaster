using Cine;
using CineMaster.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineMaster.Services
{
    public interface IReservaService
    {
        Task<List<DTOReserva>> ObtenerReservasPeliculasTerror(DateTime fechaInicio, DateTime fechaFin);
    }

    public class ReservaService : IReservaService
    {
        private readonly CineContext _context;

        public ReservaService(CineContext context)
        {
            _context = context;
        }

        public async Task<List<DTOReserva>> ObtenerReservasPeliculasTerror(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var reservas = await _context.Reservas
                    .Where(r => r.Pelicula.Genero == "Terror" && r.Fecha >= fechaInicio && r.Fecha <= fechaFin)
                    .ToListAsync();

                // Mapear las reservas a DTOs
                var DTOReserva = reservas.Select(r => new DTOReserva
                {
                    Id = r.Id,
                    Cliente = r.Cliente,
                    Pelicula = r.Pelicula,
                    Sala = r.Sala,
                    Fecha = r.Fecha
                }).ToList();

                return DTOReserva;
            }
            catch (Exception)
            {
                // Manejo de excepciones
                throw;
            }
        }
    }
}
