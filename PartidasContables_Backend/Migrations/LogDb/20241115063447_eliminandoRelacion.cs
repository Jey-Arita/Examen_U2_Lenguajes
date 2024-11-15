using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartidasContables.Migrations.LogDb
{
    /// <inheritdoc />
    public partial class eliminandoRelacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_logs_partidas_id_partida",
                schema: "dbo",
                table: "logs");

            migrationBuilder.DropTable(
                name: "detalle_partidas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "partidas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropIndex(
                name: "IX_logs_id_partida",
                schema: "dbo",
                table: "logs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    refresh_token = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    refresh_token_expire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    id_usuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    desactivada = table.Column<bool>(type: "bit", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_partidas_UserEntity_id_usuario",
                        column: x => x.id_usuario,
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
                    id_catalogo_cuenta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_partida = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tipo_movimiento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_partidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalle_partidas_catalogo_cuentas_id_catalogo_cuenta",
                        column: x => x.id_catalogo_cuenta,
                        principalSchema: "dbo",
                        principalTable: "catalogo_cuentas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_detalle_partidas_partidas_id_partida",
                        column: x => x.id_partida,
                        principalSchema: "dbo",
                        principalTable: "partidas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_logs_id_partida",
                schema: "dbo",
                table: "logs",
                column: "id_partida");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_partidas_id_catalogo_cuenta",
                schema: "dbo",
                table: "detalle_partidas",
                column: "id_catalogo_cuenta");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_partidas_id_partida",
                schema: "dbo",
                table: "detalle_partidas",
                column: "id_partida");

            migrationBuilder.CreateIndex(
                name: "IX_partidas_id_usuario",
                schema: "dbo",
                table: "partidas",
                column: "id_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_logs_partidas_id_partida",
                schema: "dbo",
                table: "logs",
                column: "id_partida",
                principalSchema: "dbo",
                principalTable: "partidas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
