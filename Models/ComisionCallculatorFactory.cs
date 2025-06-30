// ComisionCalculatorFactory.cs
using minicore_comiciones.Models;

namespace minicore_comiciones.Services
{
    public class ComisionCalculatorFactory : IComisionCalculatorFactory
    {
        public IComisionCalculator GetCalculator(Regla regla)
        {
            // Ejemplo: si amount muy alto, un bonus; si no, tarifa fija
            return regla.Amount > 800m
                 ? new BonusOverThresholdCalculator()
                 : new FixedRateCalculator();
        }
    }
}
