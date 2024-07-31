using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Progetto_Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class order : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_Prodottiordinati_ProdottoId",
                table: "Prodottiordinati",
                column: "ProdottoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodottiordinati_Prodotti_ProdottoId",
                table: "Prodottiordinati",
                column: "ProdottoId",
                principalTable: "Prodotti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodottiordinati_Prodotti_ProdottoId",
                table: "Prodottiordinati");

            migrationBuilder.DropIndex(
                name: "IX_Prodottiordinati_ProdottoId",
                table: "Prodottiordinati");

            migrationBuilder.DropColumn(
                name: "ProdottoId",
                table: "Prodottiordinati");
        }
    }
}
