using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PartidasContables.DataBase.Entities;

namespace PartidasContables.DataBase
{
    public class PartidaDbContext : IdentityDbContext<UserEntity>
    {
        public PartidaDbContext(DbContextOptions<PartidaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la entidad CatalogoCuentaEntity
            modelBuilder.Entity<CatalogoCuentaEntity>()
                .HasOne(c => c.CuentaPadre)
                .WithMany(c => c.CuentasHijas)
                .HasForeignKey(c => c.IdCuentaPadre)
                .OnDelete(DeleteBehavior.NoAction); // Evita cascada en eliminación

            modelBuilder.Entity<DetallePartidaEntity>()
            .HasOne(d => d.Partida) // Relación con PartidaEntity
            .WithMany(p => p.Detalles) // Si una Partida tiene muchos Detalles
            .HasForeignKey(d => d.IdPartida);

            // Configuración para la clave compuesta en SaldoEntity
            modelBuilder.Entity<SaldoEntity>()
                .HasKey(e => new { e.Año, e.Mes, e.MontoSaldo });

            // Configuración general
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.HasDefaultSchema("security");

            // Mapeo de tablas para Identity
            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");
        }

        public DbSet<CatalogoCuentaEntity> CatalogoCuentas { get; set; }
        public DbSet<PartidaEntity> Partidas { get; set; }
        public DbSet<DetallePartidaEntity> DetallesPartida { get; set; }
        public DbSet<SaldoEntity> Saldos { get; set; }
    }
}
