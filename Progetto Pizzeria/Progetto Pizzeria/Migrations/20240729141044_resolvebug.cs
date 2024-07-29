using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Progetto_Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class resolvebug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProdottoId",
                table: "Prodottiordinati");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdottoId",
                table: "Prodottiordinati",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
