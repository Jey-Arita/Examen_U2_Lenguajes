using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartidasContables.Migrations
{
    /// <inheritdoc />
    public partial class addTipoDt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tipo_movimiento",
                schema: "dbo",
                table: "detalle_partidas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_cuenta_padre",
                schema: "dbo",
                table: "catalogo_cuentas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipo_movimiento",
                schema: "dbo",
                table: "detalle_partidas");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_cuenta_padre",
                schema: "dbo",
                table: "catalogo_cuentas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
