using Microsoft.EntityFrameworkCore;
using PartidasContables.DataBase.Entities;

namespace PartidasContables.DataBase
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions options)
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
                .OnDelete(DeleteBehavior.NoAction); // Asegura que NO haya cascada


            // Configuración de la entidad LogEntity
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

            // Set FKs OnRestrict
            var eTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var type in eTypes)
            {
                var foreignKeys = type.GetForeignKeys();
                foreach (var foreignKey in foreignKeys)
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
    }
}
