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

            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.HasDefaultSchema("security");

            modelBuilder.Entity<UserEntity>().ToTable("users");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");


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

        public DbSet<CatalogoCuentaEntity> CatalogoCuentas { get; set; }
        public DbSet<PartidaEntity> Partidas { get; set; }
        public DbSet<DetallePartidaEntity> DetallesPartida { get; set; }
        public DbSet<SaldoEntity> Saldos { get; set; }
    }
}
