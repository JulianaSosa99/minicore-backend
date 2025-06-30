using minicore_comiciones.Models;

namespace minicore_comiciones.Services
{
    public interface IComisionCalculatorFactory
    {
        IComisionCalculator GetCalculator(Regla regla);
    }
}
