using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartidasContables.Migrations
{
    /// <inheritdoc />
    public partial class AddNumeroCuenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "numero_cuenta",
                schema: "dbo",
                table: "catalogo_cuentas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numero_cuenta",
                schema: "dbo",
                table: "catalogo_cuentas");
        }
    }
}
