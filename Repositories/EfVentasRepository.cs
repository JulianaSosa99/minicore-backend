// EfVentasRepository.cs
using Microsoft.EntityFrameworkCore;
using minicore_comiciones.DBContext;
using minicore_comiciones.Models;

namespace minicore_comiciones.Repositories
{
    public class EfVentasRepository : IVentasRepository
    {
        private readonly AppDbContext _ctx;
        public EfVentasRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<List<Venta>> GetVentasAsync(DateTime desde, DateTime hasta) =>
            _ctx.Ventas
                .Where(v => v.FechaVenta >= desde && v.FechaVenta <= hasta)
                .ToListAsync();

        public Task<Usuario> GetUsuarioAsync(int usuarioId) =>
            _ctx.Usuarios.FindAsync(usuarioId).AsTask();
    }
}
