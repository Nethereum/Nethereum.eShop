using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Nethereum.eShop.Sqlite.Catalog.Migrations
{
    public partial class RemoveBuyerAndWalletAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_BuyerAddress",
                table: "Orders");

            // SQLite does not support drop column
            // Create new temp table
            migrationBuilder.CreateTable(
                name: "TmpOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuoteId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TransactionHash = table.Column<string>(maxLength: 67, nullable: true),
                    BuyerId = table.Column<string>(maxLength: 256, nullable: false),
                    CurrencyAddress = table.Column<string>(maxLength: 43, nullable: true),
                    CurrencySymbol = table.Column<string>(maxLength: 32, nullable: true),
                    PoNumber = table.Column<long>(nullable: true),
                    PoType = table.Column<int>(nullable: false),
                    BuyerWalletAddress = table.Column<string>(maxLength: 43, nullable: true),
                    SellerId = table.Column<string>(maxLength: 32, nullable: true),
                    PoDate = table.Column<DateTimeOffset>(nullable: true),
                    OrderDate = table.Column<DateTimeOffset>(nullable: false),
                    BillTo_RecipientName = table.Column<string>(maxLength: 255, nullable: true),
                    BillTo_Street = table.Column<string>(maxLength: 180, nullable: true),
                    BillTo_City = table.Column<string>(maxLength: 100, nullable: true),
                    BillTo_State = table.Column<string>(maxLength: 60, nullable: true),
                    BillTo_Country = table.Column<string>(maxLength: 90, nullable: true),
                    BillTo_ZipCode = table.Column<string>(maxLength: 18, nullable: true),
                    ShipTo_RecipientName = table.Column<string>(maxLength: 255, nullable: true),
                    ShipTo_Street = table.Column<string>(maxLength: 180, nullable: true),
                    ShipTo_City = table.Column<string>(maxLength: 100, nullable: true),
                    ShipTo_State = table.Column<string>(maxLength: 60, nullable: true),
                    ShipTo_Country = table.Column<string>(maxLength: 90, nullable: true),
                    ShipTo_ZipCode = table.Column<string>(maxLength: 18, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TmpOrders", x => x.Id);
                });

            // Copy existing data to temp table, drop original and rename temp
            migrationBuilder.Sql("INSERT INTO TmpOrders SELECT Id, QuoteId, Status, TransactionHash, BuyerId, CurrencyAddress, CurrencySymbol, PoNumber, PoType, BuyerWalletAddress, Sellerid, PoDate, OrderDate, BillTo_RecipientName, BillTo_Street, BillTo_City, BillTo_State, BillTo_Country, BillTo_ZipCode, ShipTo_RecipientName, ShipTo_Street, ShipTo_City, ShipTo_State, ShipTo_Country, ShipTo_ZipCode  FROM Orders;");
            migrationBuilder.Sql("PRAGMA foreign_keys=\"0\"", true);
            migrationBuilder.Sql("DROP TABLE Orders", true);
            migrationBuilder.Sql("ALTER TABLE TmpOrders RENAME TO Orders", true);
            migrationBuilder.Sql("PRAGMA foreign_keys=\"1\"", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApproverAddress",
                table: "Orders",
                type: "TEXT",
                maxLength: 43,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerAddress",
                table: "Orders",
                type: "TEXT",
                maxLength: 43,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerAddress",
                table: "Orders",
                column: "BuyerAddress");
        }
    }
}
