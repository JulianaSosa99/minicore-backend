namespace minicore_comiciones.Models
{
    public class Venta
    {

        public int VentaId { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Monto { get; set; }

        // FK al vendedor
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
