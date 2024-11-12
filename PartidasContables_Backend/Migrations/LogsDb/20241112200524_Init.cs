using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartidasContables.Migrations.LogsDb
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogoCuentaEntity",
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
                    table.PrimaryKey("PK_CatalogoCuentaEntity", x => x.id);
                    table.ForeignKey(
                        name: "FK_CatalogoCuentaEntity_CatalogoCuentaEntity_IdCuentaPadre",
                        column: x => x.IdCuentaPadre,
                        principalTable: "CatalogoCuentaEntity",
                        principalColumn: "id");
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
                name: "PartidaEntity",
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
                    table.PrimaryKey("PK_PartidaEntity", x => x.id);
                    table.ForeignKey(
                        name: "FK_PartidaEntity_UserEntity_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallePartidaEntity",
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
                    table.PrimaryKey("PK_DetallePartidaEntity", x => x.id);
                    table.ForeignKey(
                        name: "FK_DetallePartidaEntity_CatalogoCuentaEntity_IdCatalogoCuenta",
                        column: x => x.IdCatalogoCuenta,
                        principalTable: "CatalogoCuentaEntity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePartidaEntity_PartidaEntity_IdPartida",
                        column: x => x.IdPartida,
                        principalTable: "PartidaEntity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
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
                    table.PrimaryKey("PK_Logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Logs_PartidaEntity_IdPartida",
                        column: x => x.IdPartida,
                        principalTable: "PartidaEntity",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogoCuentaEntity_IdCuentaPadre",
                table: "CatalogoCuentaEntity",
                column: "IdCuentaPadre");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePartidaEntity_IdCatalogoCuenta",
                table: "DetallePartidaEntity",
                column: "IdCatalogoCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePartidaEntity_IdPartida",
                table: "DetallePartidaEntity",
                column: "IdPartida");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_IdPartida",
                table: "Logs",
                column: "IdPartida");

            migrationBuilder.CreateIndex(
                name: "IX_PartidaEntity_IdUsuario",
                table: "PartidaEntity",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePartidaEntity");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "CatalogoCuentaEntity");

            migrationBuilder.DropTable(
                name: "PartidaEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
