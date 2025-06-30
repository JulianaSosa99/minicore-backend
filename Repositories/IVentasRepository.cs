using minicore_comiciones.Models;
namespace minicore_comiciones.Repositories
{
    public interface IVentasRepository
    {
        Task<List<Venta>> GetVentasAsync(DateTime desde, DateTime hasta);
        Task<Usuario> GetUsuarioAsync(int usuarioId);
    }
}
