namespace minicore_comiciones.Services
{
    public interface IComisionCalculator
    {
        decimal Calculate(decimal totalVentas, decimal porcentaje);
    }
}
