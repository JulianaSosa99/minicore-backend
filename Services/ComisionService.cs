// ComisionService.cs
using minicore_comiciones.Dtos;
using minicore_comiciones.Models;
using minicore_comiciones.Repositories;
using Microsoft.EntityFrameworkCore;
using minicore_comiciones.DBContext;

namespace minicore_comiciones.Services
{
    public class ComisionService : IComisionService
    {
        private readonly IVentasRepository _ventasRepo;
        private readonly IComisionCalculatorFactory _calcFactory;
        private readonly AppDbContext _db; // solo para reglas

        public ComisionService(
            IVentasRepository ventasRepo,
            IComisionCalculatorFactory calcFactory,
            AppDbContext db)
        {
            _ventasRepo = ventasRepo;
            _calcFactory = calcFactory;
            _db = db;
        }

        public async Task<List<ComisionDto>> CalcularAsync(DateTime desde, DateTime hasta)
        {
            var ventas = await _ventasRepo.GetVentasAsync(desde, hasta);
            var reglas = await _db.Reglas.OrderBy(r => r.Amount).ToListAsync();

            var resultado = new List<ComisionDto>();

            foreach (var group in ventas.GroupBy(v => v.UsuarioId))
            {
                var total = group.Sum(v => v.Monto);
                var regla = reglas.Where(r => r.Amount <= total).LastOrDefault();

                var usuario = await _ventasRepo.GetUsuarioAsync(group.Key);
                var nombre = usuario?.Nombre ?? "Desconocido";

                if (regla == null)
                {
                    resultado.Add(new ComisionDto
                    {
                        Nombre = nombre,
                        TotalVentas = total,
                        Porcentaje = 0m,
                        Monto = 0m
                    });
                }
                else
                {
                    var calc = _calcFactory.GetCalculator(regla);
                    var comi = calc.Calculate(total, regla.Rule);

                    resultado.Add(new ComisionDto
                    {
                        Nombre = nombre,
                        TotalVentas = total,
                        Porcentaje = regla.Rule,
                        Monto = comi
                    });
                }
            }

            return resultado;
        }
    }
}
