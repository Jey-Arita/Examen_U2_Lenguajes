﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PartidasContables.DataBase;

#nullable disable

namespace PartidasContables.Migrations.LogDb
{
    [DbContext(typeof(LogDbContext))]
    partial class LogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PartidasContables.DataBase.Entities.CatalogoCuentaEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("descripcion");

                    b.Property<Guid?>("IdCuentaPadre")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id_cuenta_padre");

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("numero_cuenta");

                    b.Property<bool>("PermiteMovimiento")
                        .HasColumnType("bit")
                        .HasColumnName("permite_movimiento");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("saldo");

                    b.Property<string>("TipoCuenta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tipo_cuenta");

                    b.HasKey("Id");

                    b.HasIndex("IdCuentaPadre");

                    b.ToTable("catalogo_cuentas", "dbo");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.DetallePartidaEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("descripcion");

                    b.Property<Guid>("IdCatalogoCuenta")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id_catalogo_cuenta");

                    b.Property<Guid>("IdPartida")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id_partida");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("monto");

                    b.Property<string>("TipoMovimiento")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tipo_movimiento");

                    b.HasKey("Id");

                    b.HasIndex("IdCatalogoCuenta");

                    b.HasIndex("IdPartida");

                    b.ToTable("detalle_partidas", "dbo");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.LogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Accion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("accion");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha");

                    b.Property<Guid?>("IdPartida")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id_partida");

                    b.Property<string>("IdUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("id_usuario");

                    b.HasKey("Id");

                    b.HasIndex("IdPartida");

                    b.ToTable("logs", "dbo");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.PartidaEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<bool>("Desactivada")
                        .HasColumnType("bit")
                        .HasColumnName("desactivada");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("descripcion");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha");

                    b.Property<string>("IdUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id_usuario");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("partidas", "dbo");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime>("RefreshTokenExpire")
                        .HasColumnType("datetime2")
                        .HasColumnName("refresh_token_expire");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserEntity");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.CatalogoCuentaEntity", b =>
                {
                    b.HasOne("PartidasContables.DataBase.Entities.CatalogoCuentaEntity", "CuentaPadre")
                        .WithMany("CuentasHijas")
                        .HasForeignKey("IdCuentaPadre")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CuentaPadre");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.DetallePartidaEntity", b =>
                {
                    b.HasOne("PartidasContables.DataBase.Entities.CatalogoCuentaEntity", "CatalogoCuenta")
                        .WithMany()
                        .HasForeignKey("IdCatalogoCuenta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PartidasContables.DataBase.Entities.PartidaEntity", "Partida")
                        .WithMany("Detalles")
                        .HasForeignKey("IdPartida")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CatalogoCuenta");

                    b.Navigation("Partida");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.LogEntity", b =>
                {
                    b.HasOne("PartidasContables.DataBase.Entities.PartidaEntity", "Partida")
                        .WithMany()
                        .HasForeignKey("IdPartida")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Partida");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.PartidaEntity", b =>
                {
                    b.HasOne("PartidasContables.DataBase.Entities.UserEntity", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.CatalogoCuentaEntity", b =>
                {
                    b.Navigation("CuentasHijas");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.PartidaEntity", b =>
                {
                    b.Navigation("Detalles");
                });
#pragma warning restore 612, 618
        }
    }
}
