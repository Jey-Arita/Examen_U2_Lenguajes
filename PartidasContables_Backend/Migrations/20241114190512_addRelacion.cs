using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartidasContables.Migrations
{
    /// <inheritdoc />
    public partial class addRelacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PartidaId",
                schema: "dbo",
                table: "partidas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_partidas_PartidaId",
                schema: "dbo",
                table: "partidas",
                column: "PartidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_partidas_partidas_PartidaId",
                schema: "dbo",
                table: "partidas",
                column: "PartidaId",
                principalSchema: "dbo",
                principalTable: "partidas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_partidas_partidas_PartidaId",
                schema: "dbo",
                table: "partidas");

            migrationBuilder.DropIndex(
                name: "IX_partidas_PartidaId",
                schema: "dbo",
                table: "partidas");

            migrationBuilder.DropColumn(
                name: "PartidaId",
                schema: "dbo",
                table: "partidas");
        }
    }
}
