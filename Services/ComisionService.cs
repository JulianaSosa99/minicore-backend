using Microsoft.EntityFrameworkCore;
using minicore_comiciones.DBContext;
using minicore_comiciones.Dtos;

namespace minicore_comiciones.Services
{
    public class ComisionService : IComisionService
    {
        private readonly AppDbContext _db;
        public ComisionService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<ComisionDto>> CalcularAsync(DateTime desde, DateTime hasta)
        {
            // 1) Sumar ventas por vendedor en rango
            var sumaPorVendedor = await _db.Ventas
                .Where(v => v.FechaVenta >= desde && v.FechaVenta <= hasta)
                .GroupBy(v => v.UsuarioId)
                .Select(g => new {
                    UsuarioId = g.Key,
                    TotalVentas = g.Sum(v => v.Monto)
                })
                .ToListAsync();

            // 2) Para cada suma, elegir la regla correcta y calcular comisión
            var reglas = await _db.Reglas
                .OrderBy(r => r.Amount)
                .ToListAsync();

            var resultado = new List<ComisionDto>();

            foreach (var s in sumaPorVendedor)
            {
                // Regla con Amount <= total, y con el Amount más alto posible
                var regla = reglas
                    .Where(r => r.Amount <= s.TotalVentas)
                    .LastOrDefault();

                var usuario = await _db.Usuarios.FindAsync(s.UsuarioId);

                if (regla is null)
                {
                    resultado.Add(new ComisionDto
                    {
                        Nombre = usuario?.Nombre ?? "Desconocido",
                        TotalVentas = s.TotalVentas,
                        Porcentaje = 0,
                        Monto = 0
                    });
                }
                else
                {
                    resultado.Add(new ComisionDto
                    {
                        Nombre = usuario?.Nombre ?? "Desconocido",
                        TotalVentas = s.TotalVentas,
                        Porcentaje = regla.Rule,
                        Monto = s.TotalVentas * regla.Rule
                    });
                }
            }

            return resultado;
        }
    }
}
