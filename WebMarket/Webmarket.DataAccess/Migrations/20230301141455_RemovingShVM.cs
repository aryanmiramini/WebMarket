using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webmarket.DataAccess.Migrations
{
    public partial class RemovingShVM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CartTotal",
                table: "ShoppingCarts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "ShoppingCarts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCarts",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCarts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_ShoppingCartId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CartTotal",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "ShoppingCarts");
        }
    }
}
