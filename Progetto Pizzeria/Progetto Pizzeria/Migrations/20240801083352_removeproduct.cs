using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Progetto_Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class removeproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodottiordinati_Ordini_OrdineId",
                table: "Prodottiordinati");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Users__3214EC0798BFEE9C",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__UserRol__3214EC07D639C897",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Roles__3214EC0797369540",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prodottiordinati",
                table: "Prodottiordinati");

            migrationBuilder.AlterColumn<int>(
                name: "OrdineId",
                table: "Prodottiordinati",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdottiOrdinati",
                table: "Prodottiordinati",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodottiordinati_Ordini_OrdineId",
                table: "Prodottiordinati",
                column: "OrdineId",
                principalTable: "Ordini",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodottiordinati_Ordini_OrdineId",
                table: "Prodottiordinati");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdottiOrdinati",
                table: "Prodottiordinati");

            migrationBuilder.AlterColumn<int>(
                name: "OrdineId",
                table: "Prodottiordinati",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Users__3214EC0798BFEE9C",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__UserRol__3214EC07D639C897",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Roles__3214EC0797369540",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prodottiordinati",
                table: "Prodottiordinati",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodottiordinati_Ordini_OrdineId",
                table: "Prodottiordinati",
                column: "OrdineId",
                principalTable: "Ordini",
                principalColumn: "Id");
        }
    }
}
