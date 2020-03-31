using Microsoft.EntityFrameworkCore.Migrations;

namespace Nethereum.eShop.Sqlite.Catalog.Migrations
{
    public partial class RemoveBuyerAndWalletAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_BuyerAddress",
                table: "Orders");

            //SQLite does not support drop column
            // TODO: implement a workaround - create new table with only required columns, copy data to new table, rename
            // https://t-heiten.net/ef-core/fix-sqlite-migration/

            //migrationBuilder.DropColumn(
            //    name: "ApproverAddress",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "BuyerAddress",
            //    table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //SQLite does not support drop column

            //migrationBuilder.AddColumn<string>(
            //    name: "ApproverAddress",
            //    table: "Orders",
            //    type: "TEXT",
            //    maxLength: 43,
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "BuyerAddress",
            //    table: "Orders",
            //    type: "TEXT",
            //    maxLength: 43,
            //    nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerAddress",
                table: "Orders",
                column: "BuyerAddress");
        }
    }
}
