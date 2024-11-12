using Microsoft.EntityFrameworkCore;
using PartidasContables.DataBase.Entities;

namespace PartidasContables.DataBase
{
    public class LogsDbContext : DbContext
    {
        public LogsDbContext(DbContextOptions options)
                : base(options)
        {

        }

        // Definimos la tabla para los logs
        public DbSet<LogEntity> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CatalogoCuentaEntity>()
            .HasOne(c => c.CuentaPadre)
            .WithMany(c => c.CuentasHijas)
            .HasForeignKey(c => c.IdCuentaPadre)
            .OnDelete(DeleteBehavior.NoAction);

            // Configuracin de la entidad LogEntity
            modelBuilder.Entity<LogEntity>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Fecha)
                    .IsRequired();

                entity.Property(e => e.IdUsuario)
                    .IsRequired();

                entity.Property(e => e.Accion)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.IdPartida)
                    .IsRequired(false); // Opcional, solo si aplica
            });
        }
    }
}
