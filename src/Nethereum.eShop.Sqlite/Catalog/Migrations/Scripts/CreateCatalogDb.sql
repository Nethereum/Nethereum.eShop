CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

CREATE TABLE "Baskets" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Baskets" PRIMARY KEY AUTOINCREMENT,
    "BuyerId" TEXT NOT NULL,
    "BuyerAddress" TEXT NOT NULL,
    "BillTo_RecipientName" TEXT NULL,
    "BillTo_Street" TEXT NULL,
    "BillTo_City" TEXT NULL,
    "BillTo_State" TEXT NULL,
    "BillTo_Country" TEXT NULL,
    "BillTo_ZipCode" TEXT NULL,
    "ShipTo_RecipientName" TEXT NULL,
    "ShipTo_Street" TEXT NULL,
    "ShipTo_City" TEXT NULL,
    "ShipTo_State" TEXT NULL,
    "ShipTo_Country" TEXT NULL,
    "ShipTo_ZipCode" TEXT NULL,
    "TransactionHash" TEXT NULL
);

CREATE TABLE "Buyers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Buyers" PRIMARY KEY AUTOINCREMENT,
    "BuyerId" TEXT NOT NULL,
    "BuyerAddress" TEXT NULL
);

CREATE TABLE "CatalogBrands" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_CatalogBrands" PRIMARY KEY AUTOINCREMENT,
    "Brand" TEXT NOT NULL
);

CREATE TABLE "CatalogTypes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_CatalogTypes" PRIMARY KEY AUTOINCREMENT,
    "Type" TEXT NOT NULL
);

CREATE TABLE "Orders" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Orders" PRIMARY KEY AUTOINCREMENT,
    "QuoteId" INTEGER NULL,
    "Status" INTEGER NOT NULL,
    "TransactionHash" TEXT NULL,
    "BuyerId" TEXT NOT NULL,
    "BuyerAddress" TEXT NULL,
    "CurrencyAddress" TEXT NULL,
    "CurrencySymbol" TEXT NULL,
    "ApproverAddress" TEXT NULL,
    "PoNumber" INTEGER NULL,
    "PoType" INTEGER NOT NULL,
    "BuyerWalletAddress" TEXT NULL,
    "SellerId" TEXT NULL,
    "PoDate" TEXT NULL,
    "OrderDate" TEXT NOT NULL,
    "BillTo_RecipientName" TEXT NULL,
    "BillTo_Street" TEXT NULL,
    "BillTo_City" TEXT NULL,
    "BillTo_State" TEXT NULL,
    "BillTo_Country" TEXT NULL,
    "BillTo_ZipCode" TEXT NULL,
    "ShipTo_RecipientName" TEXT NULL,
    "ShipTo_Street" TEXT NULL,
    "ShipTo_City" TEXT NULL,
    "ShipTo_State" TEXT NULL,
    "ShipTo_Country" TEXT NULL,
    "ShipTo_ZipCode" TEXT NULL
);

CREATE TABLE "Quotes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Quotes" PRIMARY KEY AUTOINCREMENT,
    "Status" INTEGER NOT NULL,
    "Date" TEXT NOT NULL,
    "Expiry" TEXT NOT NULL,
    "TransactionHash" TEXT NULL,
    "BuyerAddress" TEXT NULL,
    "CurrencySymbol" TEXT NULL,
    "CurrencyAddress" TEXT NULL,
    "ApproverAddress" TEXT NULL,
    "PoNumber" INTEGER NULL,
    "PoType" INTEGER NOT NULL,
    "BuyerWalletAddress" TEXT NULL,
    "SellerId" TEXT NULL,
    "BuyerId" TEXT NOT NULL,
    "BillTo_RecipientName" TEXT NULL,
    "BillTo_Street" TEXT NULL,
    "BillTo_City" TEXT NULL,
    "BillTo_State" TEXT NULL,
    "BillTo_Country" TEXT NULL,
    "BillTo_ZipCode" TEXT NULL,
    "ShipTo_RecipientName" TEXT NULL,
    "ShipTo_Street" TEXT NULL,
    "ShipTo_City" TEXT NULL,
    "ShipTo_State" TEXT NULL,
    "ShipTo_Country" TEXT NULL,
    "ShipTo_ZipCode" TEXT NULL
);

CREATE TABLE "BasketItems" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_BasketItems" PRIMARY KEY AUTOINCREMENT,
    "UnitPrice" decimal(18,2) NOT NULL,
    "Quantity" INTEGER NOT NULL,
    "CatalogItemId" INTEGER NOT NULL,
    "BasketId" INTEGER NOT NULL,
    CONSTRAINT "FK_BasketItems_Baskets_BasketId" FOREIGN KEY ("BasketId") REFERENCES "Baskets" ("Id") ON DELETE CASCADE
);

CREATE TABLE "BuyerPostalAddress" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_BuyerPostalAddress" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NULL,
    "PostalAddress_RecipientName" TEXT NULL,
    "PostalAddress_Street" TEXT NULL,
    "PostalAddress_City" TEXT NULL,
    "PostalAddress_State" TEXT NULL,
    "PostalAddress_Country" TEXT NULL,
    "PostalAddress_ZipCode" TEXT NULL,
    "BuyerId" INTEGER NULL,
    CONSTRAINT "FK_BuyerPostalAddress_Buyers_BuyerId" FOREIGN KEY ("BuyerId") REFERENCES "Buyers" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Catalog" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Catalog" PRIMARY KEY AUTOINCREMENT,
    "Status" INTEGER NOT NULL,
    "Gtin" TEXT NOT NULL,
    "GtinRegistryId" INTEGER NULL,
    "Name" TEXT NOT NULL,
    "Description" TEXT NULL,
    "Price" decimal(18,2) NOT NULL,
    "Unit" TEXT NULL,
    "CatalogTypeId" INTEGER NOT NULL,
    "CatalogBrandId" INTEGER NOT NULL,
    "PictureUri" TEXT NULL,
    "PictureSmallUri" TEXT NULL,
    "PictureMediumUri" TEXT NULL,
    "PictureLargeUri" TEXT NULL,
    "AttributeJson" TEXT NULL,
    "Rank" INTEGER NOT NULL,
    "Height" INTEGER NOT NULL,
    "Width" INTEGER NOT NULL,
    "Depth" INTEGER NOT NULL,
    "Weight" INTEGER NOT NULL,
    CONSTRAINT "FK_Catalog_CatalogBrands_CatalogBrandId" FOREIGN KEY ("CatalogBrandId") REFERENCES "CatalogBrands" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Catalog_CatalogTypes_CatalogTypeId" FOREIGN KEY ("CatalogTypeId") REFERENCES "CatalogTypes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "OrderItems" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_OrderItems" PRIMARY KEY AUTOINCREMENT,
    "Status" INTEGER NOT NULL,
    "ItemOrdered_CatalogItemId" INTEGER NULL,
    "ItemOrdered_Gtin" TEXT NULL,
    "ItemOrdered_GtinRegistryId" INTEGER NULL,
    "ItemOrdered_ProductName" TEXT NULL,
    "ItemOrdered_PictureUri" TEXT NULL,
    "UnitPrice" decimal(18,2) NOT NULL,
    "Quantity" INTEGER NOT NULL,
    "Unit" TEXT NULL,
    "PoItemStatus" INTEGER NULL,
    "PoItemNumber" INTEGER NULL,
    "GoodsIssueDate" TEXT NULL,
    "ActualEscrowReleaseDate" TEXT NULL,
    "PlannedEscrowReleaseDate" TEXT NULL,
    "IsEscrowReleased" INTEGER NULL,
    "QuantitySymbol" TEXT NULL,
    "QuantityAddress" TEXT NULL,
    "CurrencyValue" TEXT NULL,
    "OrderId" INTEGER NULL,
    CONSTRAINT "FK_OrderItems_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "QuoteItems" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_QuoteItems" PRIMARY KEY AUTOINCREMENT,
    "ItemOrdered_CatalogItemId" INTEGER NULL,
    "ItemOrdered_Gtin" TEXT NULL,
    "ItemOrdered_GtinRegistryId" INTEGER NULL,
    "ItemOrdered_ProductName" TEXT NULL,
    "ItemOrdered_PictureUri" TEXT NULL,
    "UnitPrice" decimal(18,2) NOT NULL,
    "Quantity" INTEGER NOT NULL,
    "Unit" TEXT NULL,
    "PoItemNumber" INTEGER NULL,
    "EscrowReleaseDate" TEXT NULL,
    "QuantitySymbol" TEXT NULL,
    "QuantityAddress" TEXT NULL,
    "CurrencyValue" TEXT NULL,
    "QuoteId" INTEGER NULL,
    CONSTRAINT "FK_QuoteItems_Quotes_QuoteId" FOREIGN KEY ("QuoteId") REFERENCES "Quotes" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "Stock" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Stock" PRIMARY KEY AUTOINCREMENT,
    "CatalogItemId" INTEGER NOT NULL,
    "Location" TEXT NOT NULL,
    "Quantity" INTEGER NOT NULL,
    CONSTRAINT "FK_Stock_Catalog_CatalogItemId" FOREIGN KEY ("CatalogItemId") REFERENCES "Catalog" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_BasketItems_BasketId" ON "BasketItems" ("BasketId");

CREATE INDEX "IX_Baskets_BuyerAddress" ON "Baskets" ("BuyerAddress");

CREATE INDEX "IX_Baskets_BuyerId" ON "Baskets" ("BuyerId");

CREATE INDEX "IX_BuyerPostalAddress_BuyerId" ON "BuyerPostalAddress" ("BuyerId");

CREATE UNIQUE INDEX "IX_Buyers_BuyerAddress" ON "Buyers" ("BuyerAddress");

CREATE UNIQUE INDEX "IX_Buyers_BuyerId" ON "Buyers" ("BuyerId");

CREATE INDEX "IX_Catalog_CatalogBrandId" ON "Catalog" ("CatalogBrandId");

CREATE INDEX "IX_Catalog_CatalogTypeId" ON "Catalog" ("CatalogTypeId");

CREATE INDEX "IX_OrderItems_OrderId" ON "OrderItems" ("OrderId");

CREATE INDEX "IX_Orders_BuyerAddress" ON "Orders" ("BuyerAddress");

CREATE INDEX "IX_Orders_BuyerId" ON "Orders" ("BuyerId");

CREATE INDEX "IX_QuoteItems_QuoteId" ON "QuoteItems" ("QuoteId");

CREATE INDEX "IX_Quotes_BuyerAddress" ON "Quotes" ("BuyerAddress");

CREATE INDEX "IX_Quotes_BuyerId" ON "Quotes" ("BuyerId");

CREATE INDEX "IX_Stock_CatalogItemId" ON "Stock" ("CatalogItemId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200311151703_InitialCreate', '3.1.2');

ALTER TABLE "Buyers" ADD "BuyerWalletAddress" TEXT NULL;

CREATE UNIQUE INDEX "IX_Buyers_BuyerWalletAddress" ON "Buyers" ("BuyerWalletAddress");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20200311172413_BuyerWalletAddress', '3.1.2');

