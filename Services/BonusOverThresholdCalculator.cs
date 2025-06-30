// BonusOverThresholdCalculator.cs
namespace minicore_comiciones.Services
{
    public class BonusOverThresholdCalculator : IComisionCalculator
    {
        public decimal Calculate(decimal totalVentas, decimal porcentaje)
            => totalVentas > 1000m
               ? (totalVentas * porcentaje) + 50m
               : totalVentas * porcentaje;
    }
}
