using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minicore_comiciones.DBContext;
using minicore_comiciones.Dtos;
using minicore_comiciones.Models;

namespace minicore_comiciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _db;
        public VentasController(AppDbContext db) => _db = db;

      
        [HttpGet]
        public async Task<ActionResult<List<VentaDto>>> GetAll()
        {
            var lista = await _db.Ventas
               .Include(v => v.Usuario)
               .Select(v => new VentaDto
               {
                   VentaId = v.VentaId,
                   Fecha = v.FechaVenta,
                   Monto = v.Monto,
                   Vendedor = v.Usuario.Nombre
               })
               .ToListAsync();

            return Ok(lista);
        }


    }

}
