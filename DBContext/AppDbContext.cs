using Microsoft.EntityFrameworkCore;
using minicore_comiciones.Models;

namespace minicore_comiciones.DBContext
{
    public class AppDbContext : DbContext

    {

        public AppDbContext(DbContextOptions<AppDbContext> opts)
        : base(opts) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Regla> Reglas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Si tus tablas ya existen, asegúrate de mapear nombres correctos:
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Venta>().ToTable("Ventas");
            modelBuilder.Entity<Regla>().ToTable("Reglas").Property(r => r.Rule)
     .HasColumnName("Rule");
        }
    }
}
