using minicore_comiciones.Dtos;

namespace minicore_comiciones.Services
{
    public interface IComisionService
    {
        Task<List<ComisionDto>> CalcularAsync(DateTime desde, DateTime hasta);
    }
}
