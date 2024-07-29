using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Progetto_Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class FKprodottiordinati : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdottoId",
                table: "Prodottiordinati",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdottoId",
                table: "Prodottiordinati");
        }
    }
}
