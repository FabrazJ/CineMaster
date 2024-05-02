using Cine;
using CineMaster.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineMaster.Services
{
    public interface ICarteleraService
    {
        Task<bool> CancelarCartelera(int carteleraId);
        Task<List<DTOButacasPorSala>> ObtenerButacasPorSalaEnCarteleraDelDiaActual();

    }

    public class CarteleraService : ICarteleraService
    {
        private readonly CineContext _context;

        public CarteleraService(CineContext context)
        {
            _context = context;
        }

        public async Task<bool> CancelarCartelera(int carteleraId)
        {
            try
            {
                // Obtener la cartelera a cancelar
                var cartelera = await _context.Carteleras.FindAsync(carteleraId);

                if (cartelera != null)
                {
                    // Cancelar la cartelera
                    _context.Carteleras.Remove(cartelera);

                    // Eliminar las reservas asociadas
                    // (Aquí deberías agregar la lógica para eliminar las reservas de esta cartelera)

                    // Guardar los cambios en la base de datos
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones
                throw;
            }
        }

        public Task<List<DTOButacasPorSala>> ObtenerButacasPorSalaEnCarteleraDelDiaActual()
        {
            throw new NotImplementedException();
        }
    }
}
