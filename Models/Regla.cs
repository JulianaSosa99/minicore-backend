namespace minicore_comiciones.Models
{
    public class Regla
    {
        public int ReglaId { get; set; }
        public decimal Amount { get; set; }
        public decimal Rule { get; set; }  // porcentaje: e.g. 0.06, 0.08…

    }
}
