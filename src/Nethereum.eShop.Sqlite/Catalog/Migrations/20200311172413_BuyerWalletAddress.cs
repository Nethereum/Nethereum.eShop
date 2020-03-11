using Microsoft.EntityFrameworkCore.Migrations;

namespace Nethereum.eShop.Sqlite.Catalog.Migrations
{
    public partial class BuyerWalletAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerWalletAddress",
                table: "Buyers",
                maxLength: 43,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_BuyerWalletAddress",
                table: "Buyers",
                column: "BuyerWalletAddress",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Buyers_BuyerWalletAddress",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "BuyerWalletAddress",
                table: "Buyers");
        }
    }
}
