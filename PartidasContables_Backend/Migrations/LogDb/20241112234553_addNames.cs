using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartidasContables.Migrations.LogDb
{
    /// <inheritdoc />
    public partial class addNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "catalogo_cuentas",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaldoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdCuentaPadre = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermiteMovimiento = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalogo_cuentas", x => x.id);
                    table.ForeignKey(
                        name: "FK_catalogo_cuentas_catalogo_cuentas_IdCuentaPadre",
                        column: x => x.IdCuentaPadre,
                        principalSchema: "dbo",
                        principalTable: "catalogo_cuentas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    refresh_token_expire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "partidas",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EstaEliminada = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_partidas_UserEntity_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "detalle_partidas",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPartida = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCatalogoCuenta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoOperacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_partidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalle_partidas_catalogo_cuentas_IdCatalogoCuenta",
                        column: x => x.IdCatalogoCuenta,
                        principalSchema: "dbo",
                        principalTable: "catalogo_cuentas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detalle_partidas_partidas_IdPartida",
                        column: x => x.IdPartida,
                        principalSchema: "dbo",
                        principalTable: "partidas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdPartida = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_logs_partidas_IdPartida",
                        column: x => x.IdPartida,
                        principalSchema: "dbo",
                        principalTable: "partidas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_catalogo_cuentas_IdCuentaPadre",
                schema: "dbo",
                table: "catalogo_cuentas",
                column: "IdCuentaPadre");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_partidas_IdCatalogoCuenta",
                schema: "dbo",
                table: "detalle_partidas",
                column: "IdCatalogoCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_partidas_IdPartida",
                schema: "dbo",
                table: "detalle_partidas",
                column: "IdPartida");

            migrationBuilder.CreateIndex(
                name: "IX_logs_IdPartida",
                schema: "dbo",
                table: "logs",
                column: "IdPartida");

            migrationBuilder.CreateIndex(
                name: "IX_partidas_IdUsuario",
                schema: "dbo",
                table: "partidas",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalle_partidas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "logs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "catalogo_cuentas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "partidas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
