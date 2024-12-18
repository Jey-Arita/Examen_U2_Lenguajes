﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PartidasContables.DataBase;

#nullable disable

namespace PartidasContables.Migrations.LogDb
{
    [DbContext(typeof(LogDbContext))]
    [Migration("20241115072746_addIdCuenta")]
    partial class addIdCuenta
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid?>("IdCuenta")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id_cuenta");

                    b.Property<Guid?>("IdPartida")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id_partida");

                    b.Property<string>("IdUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("id_usuario");

                    b.HasKey("Id");

                    b.ToTable("logs", "dbo");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.CatalogoCuentaEntity", b =>
                {
                    b.HasOne("PartidasContables.DataBase.Entities.CatalogoCuentaEntity", "CuentaPadre")
                        .WithMany("CuentasHijas")
                        .HasForeignKey("IdCuentaPadre")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CuentaPadre");
                });

            modelBuilder.Entity("PartidasContables.DataBase.Entities.CatalogoCuentaEntity", b =>
                {
                    b.Navigation("CuentasHijas");
                });
#pragma warning restore 612, 618
        }
    }
}
