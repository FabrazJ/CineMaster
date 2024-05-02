using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cine;
using CineMaster.DTO;
using Microsoft.EntityFrameworkCore;

namespace CineMaster.Services
{
    public class CarteleraService : ICarteleraService
    {
        private readonly CineContext _context;

        public CarteleraService(CineContext context)
        {
            _context = context;
        }

        public async Task<List<DTOButacasPorSala>> ObtenerButacasPorSalaEnCarteleraDelDiaActual()
        {
            var today = DateTime.Today;

            var butacasPorSala = await _context.Billboards
                .Where(b => b.Date.Date == today)
                .Include(b => b.Room)
                .Select(b => new DTOButacasPorSala
                {
                    Sala = b.Room.Name,
                    ButacasDisponibles = b.Room.Seats.Count(s => s.Status),
                    ButacasOcupadas = b.Room.Seats.Count(s => !s.Status)
                })
                .ToListAsync();

            return butacasPorSala;
        }
    }
}
