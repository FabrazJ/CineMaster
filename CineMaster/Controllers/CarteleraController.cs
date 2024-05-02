using Cine;
using CineMaster.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarteleraController : Controller
    {
        private readonly CineContext _context;

        public CarteleraController(CineContext context)
        {
            _context = context;
        }

        private readonly ICarteleraService _carteleraService;

        public CarteleraController(ICarteleraService carteleraService)
        {
            _carteleraService = carteleraService;
        }


        // Obtener todas las funciones de la cartelera
        [HttpGet]
        public async Task<IActionResult> GetAllBillboards()
        {
            var billboards = await _context.Billboards.ToListAsync();
            return Ok(billboards);
        }

        public async Task<IActionResult> ObtenerButacasPorSalaEnCarteleraDelDiaActual()
        {
            var butacasPorSala = await _carteleraService.ObtenerButacasPorSalaEnCarteleraDelDiaActual();
            return Ok(butacasPorSala);
        }

        // Obtener una función de la cartelera por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillboardById(int id)
        {
            var billboard = await _context.Billboards.FindAsync(id);
            if (billboard == null)
            {
                return NotFound();
            }
            return Ok(billboard);
        }

        // Crear una nueva función de la cartelera
        [HttpPost]
        public async Task<IActionResult> CreateBillboard(BillboardEntity billboard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Billboards.Add(billboard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBillboardById), new { id = billboard.Id }, billboard);
        }

        // Actualizar una función de la cartelera existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBillboard(int id, BillboardEntity billboard)
        {
            if (id != billboard.Id)
            {
                return BadRequest();
            }

            _context.Entry(billboard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillboardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Eliminar una función de la cartelera
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBillboard(int id)
        //{
        //    var billboard = await _context.Billboards.FindAsync(id);
        //    if (billboard == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Billboards.Remove(billboard);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private readonly ICarteleraService _carteleraService;

        public CarteleraController(ICarteleraService carteleraService)
        {
            _carteleraService = carteleraService;
        }

        [HttpDelete("{carteleraId}")]
        public async Task<IActionResult> CancelarCartelera(int carteleraId)
        {
            var result = await _carteleraService.CancelarCartelera(carteleraId);

            if (result)
            {
                return Ok("Cartelera cancelada exitosamente.");
            }
            else
            {
                return NotFound("No se encontró la cartelera especificada.");
            }
        }
        private bool BillboardExists(int id)
        {
            return _context.Billboards.Any(e => e.Id == id);
        }
    }
}
