using Microsoft.EntityFrameworkCore.Migrations;

namespace Nethereum.eShop.MySql.Catalog.Migrations
{
    public partial class RemoveBuyerAndWalletAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_BuyerAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ApproverAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerAddress",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApproverAddress",
                table: "Orders",
                type: "varchar(43) CHARACTER SET utf8mb4",
                maxLength: 43,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerAddress",
                table: "Orders",
                type: "varchar(43) CHARACTER SET utf8mb4",
                maxLength: 43,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerAddress",
                table: "Orders",
                column: "BuyerAddress");
        }
    }
}
