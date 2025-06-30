// FixedRateCalculator.cs
namespace minicore_comiciones.Services
{
    public class FixedRateCalculator : IComisionCalculator
    {
        public decimal Calculate(decimal totalVentas, decimal porcentaje)
            => totalVentas * porcentaje;
    }
}
