using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nethereum.eShop.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "catalog_brand_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "catalog_type_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "stock_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<string>(maxLength: 256, nullable: false),
                    BuyerAddress = table.Column<string>(maxLength: 43, nullable: false),
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
                    ShipTo_ZipCode = table.Column<string>(maxLength: 18, nullable: true),
                    TransactionHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buyers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<string>(maxLength: 256, nullable: false),
                    BuyerAddress = table.Column<string>(maxLength: 43, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogBrands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Brand = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteId = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TransactionHash = table.Column<string>(maxLength: 67, nullable: true),
                    BuyerId = table.Column<string>(maxLength: 256, nullable: false),
                    BuyerAddress = table.Column<string>(maxLength: 43, nullable: true),
                    CurrencyAddress = table.Column<string>(nullable: true),
                    CurrencySymbol = table.Column<string>(nullable: true),
                    ApproverAddress = table.Column<string>(maxLength: 43, nullable: true),
                    PoNumber = table.Column<long>(nullable: true),
                    PoType = table.Column<int>(nullable: false),
                    BuyerWalletAddress = table.Column<string>(maxLength: 43, nullable: true),
                    SellerId = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Expiry = table.Column<DateTimeOffset>(nullable: false),
                    TransactionHash = table.Column<string>(maxLength: 67, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 43, nullable: true),
                    CurrencySymbol = table.Column<string>(nullable: true),
                    CurrencyAddress = table.Column<string>(nullable: true),
                    ApproverAddress = table.Column<string>(maxLength: 43, nullable: true),
                    PoNumber = table.Column<long>(nullable: true),
                    PoType = table.Column<int>(nullable: false),
                    BuyerWalletAddress = table.Column<string>(maxLength: 43, nullable: true),
                    SellerId = table.Column<string>(nullable: true),
                    BuyerId = table.Column<string>(maxLength: 256, nullable: false),
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
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CatalogItemId = table.Column<int>(nullable: false),
                    BasketId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyerPostalAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PostalAddress_RecipientName = table.Column<string>(maxLength: 255, nullable: true),
                    PostalAddress_Street = table.Column<string>(maxLength: 180, nullable: true),
                    PostalAddress_City = table.Column<string>(maxLength: 100, nullable: true),
                    PostalAddress_State = table.Column<string>(maxLength: 60, nullable: true),
                    PostalAddress_Country = table.Column<string>(maxLength: 90, nullable: true),
                    PostalAddress_ZipCode = table.Column<string>(maxLength: 18, nullable: true),
                    BuyerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerPostalAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerPostalAddress_Buyers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Buyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Gtin = table.Column<string>(maxLength: 14, nullable: false),
                    GtinRegistryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(maxLength: 8, nullable: true),
                    CatalogTypeId = table.Column<int>(nullable: false),
                    CatalogBrandId = table.Column<int>(nullable: false),
                    PictureUri = table.Column<string>(nullable: true),
                    PictureSmallUri = table.Column<string>(nullable: true),
                    PictureMediumUri = table.Column<string>(nullable: true),
                    PictureLargeUri = table.Column<string>(nullable: true),
                    AttributeJson = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogBrands_CatalogBrandId",
                        column: x => x.CatalogBrandId,
                        principalTable: "CatalogBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Catalog_CatalogTypes_CatalogTypeId",
                        column: x => x.CatalogTypeId,
                        principalTable: "CatalogTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(nullable: false),
                    ItemOrdered_CatalogItemId = table.Column<int>(nullable: true),
                    ItemOrdered_Gtin = table.Column<string>(nullable: true),
                    ItemOrdered_GtinRegistryId = table.Column<int>(nullable: true),
                    ItemOrdered_ProductName = table.Column<string>(maxLength: 50, nullable: true),
                    ItemOrdered_PictureUri = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    PoItemStatus = table.Column<int>(nullable: true),
                    PoItemNumber = table.Column<int>(nullable: true),
                    GoodsIssueDate = table.Column<DateTimeOffset>(nullable: true),
                    ActualEscrowReleaseDate = table.Column<DateTimeOffset>(nullable: true),
                    PlannedEscrowReleaseDate = table.Column<DateTimeOffset>(nullable: true),
                    IsEscrowReleased = table.Column<bool>(nullable: true),
                    QuantitySymbol = table.Column<string>(nullable: true),
                    QuantityAddress = table.Column<string>(nullable: true),
                    CurrencyValue = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuoteItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemOrdered_CatalogItemId = table.Column<int>(nullable: true),
                    ItemOrdered_Gtin = table.Column<string>(nullable: true),
                    ItemOrdered_GtinRegistryId = table.Column<int>(nullable: true),
                    ItemOrdered_ProductName = table.Column<string>(maxLength: 50, nullable: true),
                    ItemOrdered_PictureUri = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: true),
                    PoItemNumber = table.Column<int>(nullable: true),
                    EscrowReleaseDate = table.Column<DateTimeOffset>(nullable: true),
                    QuantitySymbol = table.Column<string>(nullable: true),
                    QuantityAddress = table.Column<string>(nullable: true),
                    CurrencyValue = table.Column<string>(nullable: true),
                    QuoteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuoteItems_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CatalogItemId = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Catalog_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "Catalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_BuyerAddress",
                table: "Baskets",
                column: "BuyerAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_BuyerId",
                table: "Baskets",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerPostalAddress_BuyerId",
                table: "BuyerPostalAddress",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_BuyerAddress",
                table: "Buyers",
                column: "BuyerAddress",
                unique: true,
                filter: "[BuyerAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_BuyerId",
                table: "Buyers",
                column: "BuyerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogBrandId",
                table: "Catalog",
                column: "CatalogBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogTypeId",
                table: "Catalog",
                column: "CatalogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerAddress",
                table: "Orders",
                column: "BuyerAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteItems_QuoteId",
                table: "QuoteItems",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_BuyerAddress",
                table: "Quotes",
                column: "BuyerAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_BuyerId",
                table: "Quotes",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_CatalogItemId",
                table: "Stock",
                column: "CatalogItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "BuyerPostalAddress");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "QuoteItems");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Buyers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "CatalogBrands");

            migrationBuilder.DropTable(
                name: "CatalogTypes");

            migrationBuilder.DropSequence(
                name: "catalog_brand_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_hilo");

            migrationBuilder.DropSequence(
                name: "catalog_type_hilo");

            migrationBuilder.DropSequence(
                name: "stock_hilo");
        }
    }
}
