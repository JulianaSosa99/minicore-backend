namespace minicore_comiciones.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }

        public ICollection<Venta> Ventas { get; set; }
    }
}
