using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CineMaster.Services;
using CineMaster;
using CineMaster.DTO;

namespace Cine.Services
{
    public class ReservaService : IReservaService
    {
        private readonly CineContext _context;

        public ReservaService(CineContext context)
        {
            _context = context;
        }

        public async Task<List<DTOReserva>> ObtenerReservasDeGeneroEnFechas(string genero, DateTime fechaInicio, DateTime fechaFin)
        {
            var reservas = await _context.Reservas
                .Include(r => r.Pelicula) // Asegúrate de que el modelo Reserva tenga una propiedad Pelicula y esté incluida en el contexto
                .Where(r => r.Pelicula.Genero == genero && r.Fecha >= fechaInicio && r.Fecha <= fechaFin)
                .ToListAsync();

            return reservas;
        }

        public Task<List<DTOReserva>> ObtenerReservasPeliculasTerror(DateTime fechaInicio, DateTime fechaFin)
        {
            throw new NotImplementedException();
        }
    }
}
