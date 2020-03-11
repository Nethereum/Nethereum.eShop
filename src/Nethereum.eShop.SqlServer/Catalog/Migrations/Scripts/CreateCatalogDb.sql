IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE SEQUENCE [catalog_brand_hilo] START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE SEQUENCE [catalog_hilo] START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE SEQUENCE [catalog_type_hilo] START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE SEQUENCE [stock_hilo] START WITH 1 INCREMENT BY 10 NO MINVALUE NO MAXVALUE NO CYCLE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [Baskets] (
        [Id] int NOT NULL IDENTITY,
        [BuyerId] nvarchar(256) NOT NULL,
        [BuyerAddress] nvarchar(43) NOT NULL,
        [BillTo_RecipientName] nvarchar(255) NULL,
        [BillTo_Street] nvarchar(180) NULL,
        [BillTo_City] nvarchar(100) NULL,
        [BillTo_State] nvarchar(60) NULL,
        [BillTo_Country] nvarchar(90) NULL,
        [BillTo_ZipCode] nvarchar(18) NULL,
        [ShipTo_RecipientName] nvarchar(255) NULL,
        [ShipTo_Street] nvarchar(180) NULL,
        [ShipTo_City] nvarchar(100) NULL,
        [ShipTo_State] nvarchar(60) NULL,
        [ShipTo_Country] nvarchar(90) NULL,
        [ShipTo_ZipCode] nvarchar(18) NULL,
        [TransactionHash] nvarchar(67) NULL,
        CONSTRAINT [PK_Baskets] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [Buyers] (
        [Id] int NOT NULL IDENTITY,
        [BuyerId] nvarchar(256) NOT NULL,
        [BuyerAddress] nvarchar(43) NULL,
        CONSTRAINT [PK_Buyers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [CatalogBrands] (
        [Id] int NOT NULL,
        [Brand] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_CatalogBrands] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [CatalogTypes] (
        [Id] int NOT NULL,
        [Type] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_CatalogTypes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [QuoteId] int NULL,
        [Status] int NOT NULL,
        [TransactionHash] nvarchar(67) NULL,
        [BuyerId] nvarchar(256) NOT NULL,
        [BuyerAddress] nvarchar(43) NULL,
        [CurrencyAddress] nvarchar(43) NULL,
        [CurrencySymbol] nvarchar(32) NULL,
        [ApproverAddress] nvarchar(43) NULL,
        [PoNumber] bigint NULL,
        [PoType] int NOT NULL,
        [BuyerWalletAddress] nvarchar(43) NULL,
        [SellerId] nvarchar(32) NULL,
        [PoDate] datetimeoffset NULL,
        [OrderDate] datetimeoffset NOT NULL,
        [BillTo_RecipientName] nvarchar(255) NULL,
        [BillTo_Street] nvarchar(180) NULL,
        [BillTo_City] nvarchar(100) NULL,
        [BillTo_State] nvarchar(60) NULL,
        [BillTo_Country] nvarchar(90) NULL,
        [BillTo_ZipCode] nvarchar(18) NULL,
        [ShipTo_RecipientName] nvarchar(255) NULL,
        [ShipTo_Street] nvarchar(180) NULL,
        [ShipTo_City] nvarchar(100) NULL,
        [ShipTo_State] nvarchar(60) NULL,
        [ShipTo_Country] nvarchar(90) NULL,
        [ShipTo_ZipCode] nvarchar(18) NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [Quotes] (
        [Id] int NOT NULL IDENTITY,
        [Status] int NOT NULL,
        [Date] datetimeoffset NOT NULL,
        [Expiry] datetimeoffset NOT NULL,
        [TransactionHash] nvarchar(67) NULL,
        [BuyerAddress] nvarchar(43) NULL,
        [CurrencySymbol] nvarchar(32) NULL,
        [CurrencyAddress] nvarchar(43) NULL,
        [ApproverAddress] nvarchar(43) NULL,
        [PoNumber] bigint NULL,
        [PoType] int NOT NULL,
        [BuyerWalletAddress] nvarchar(43) NULL,
        [SellerId] nvarchar(32) NULL,
        [BuyerId] nvarchar(256) NOT NULL,
        [BillTo_RecipientName] nvarchar(255) NULL,
        [BillTo_Street] nvarchar(180) NULL,
        [BillTo_City] nvarchar(100) NULL,
        [BillTo_State] nvarchar(60) NULL,
        [BillTo_Country] nvarchar(90) NULL,
        [BillTo_ZipCode] nvarchar(18) NULL,
        [ShipTo_RecipientName] nvarchar(255) NULL,
        [ShipTo_Street] nvarchar(180) NULL,
        [ShipTo_City] nvarchar(100) NULL,
        [ShipTo_State] nvarchar(60) NULL,
        [ShipTo_Country] nvarchar(90) NULL,
        [ShipTo_ZipCode] nvarchar(18) NULL,
        CONSTRAINT [PK_Quotes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [BasketItems] (
        [Id] int NOT NULL IDENTITY,
        [UnitPrice] decimal(18,2) NOT NULL,
        [Quantity] int NOT NULL,
        [CatalogItemId] int NOT NULL,
        [BasketId] int NOT NULL,
        CONSTRAINT [PK_BasketItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BasketItems_Baskets_BasketId] FOREIGN KEY ([BasketId]) REFERENCES [Baskets] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [BuyerPostalAddress] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [PostalAddress_RecipientName] nvarchar(255) NULL,
        [PostalAddress_Street] nvarchar(180) NULL,
        [PostalAddress_City] nvarchar(100) NULL,
        [PostalAddress_State] nvarchar(60) NULL,
        [PostalAddress_Country] nvarchar(90) NULL,
        [PostalAddress_ZipCode] nvarchar(18) NULL,
        [BuyerId] int NULL,
        CONSTRAINT [PK_BuyerPostalAddress] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BuyerPostalAddress_Buyers_BuyerId] FOREIGN KEY ([BuyerId]) REFERENCES [Buyers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [Catalog] (
        [Id] int NOT NULL,
        [Status] int NOT NULL,
        [Gtin] nvarchar(14) NOT NULL,
        [GtinRegistryId] int NULL,
        [Name] nvarchar(50) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Price] decimal(18,2) NOT NULL,
        [Unit] nvarchar(8) NULL,
        [CatalogTypeId] int NOT NULL,
        [CatalogBrandId] int NOT NULL,
        [PictureUri] nvarchar(512) NULL,
        [PictureSmallUri] nvarchar(512) NULL,
        [PictureMediumUri] nvarchar(512) NULL,
        [PictureLargeUri] nvarchar(512) NULL,
        [AttributeJson] nvarchar(max) NULL,
        [Rank] int NOT NULL,
        [Height] int NOT NULL,
        [Width] int NOT NULL,
        [Depth] int NOT NULL,
        [Weight] int NOT NULL,
        CONSTRAINT [PK_Catalog] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Catalog_CatalogBrands_CatalogBrandId] FOREIGN KEY ([CatalogBrandId]) REFERENCES [CatalogBrands] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Catalog_CatalogTypes_CatalogTypeId] FOREIGN KEY ([CatalogTypeId]) REFERENCES [CatalogTypes] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [OrderItems] (
        [Id] int NOT NULL IDENTITY,
        [Status] int NOT NULL,
        [ItemOrdered_CatalogItemId] int NULL,
        [ItemOrdered_Gtin] nvarchar(14) NULL,
        [ItemOrdered_GtinRegistryId] int NULL,
        [ItemOrdered_ProductName] nvarchar(50) NULL,
        [ItemOrdered_PictureUri] nvarchar(512) NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        [Quantity] int NOT NULL,
        [Unit] nvarchar(50) NULL,
        [PoItemStatus] int NULL,
        [PoItemNumber] int NULL,
        [GoodsIssueDate] datetimeoffset NULL,
        [ActualEscrowReleaseDate] datetimeoffset NULL,
        [PlannedEscrowReleaseDate] datetimeoffset NULL,
        [IsEscrowReleased] bit NULL,
        [QuantitySymbol] nvarchar(32) NULL,
        [QuantityAddress] nvarchar(43) NULL,
        [CurrencyValue] nvarchar(100) NULL,
        [OrderId] int NULL,
        CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [QuoteItems] (
        [Id] int NOT NULL IDENTITY,
        [ItemOrdered_CatalogItemId] int NULL,
        [ItemOrdered_Gtin] nvarchar(14) NULL,
        [ItemOrdered_GtinRegistryId] int NULL,
        [ItemOrdered_ProductName] nvarchar(50) NULL,
        [ItemOrdered_PictureUri] nvarchar(512) NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        [Quantity] int NOT NULL,
        [Unit] nvarchar(max) NULL,
        [PoItemNumber] int NULL,
        [EscrowReleaseDate] datetimeoffset NULL,
        [QuantitySymbol] nvarchar(max) NULL,
        [QuantityAddress] nvarchar(max) NULL,
        [CurrencyValue] nvarchar(max) NULL,
        [QuoteId] int NULL,
        CONSTRAINT [PK_QuoteItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_QuoteItems_Quotes_QuoteId] FOREIGN KEY ([QuoteId]) REFERENCES [Quotes] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE TABLE [Stock] (
        [Id] int NOT NULL,
        [CatalogItemId] int NOT NULL,
        [Location] nvarchar(50) NOT NULL,
        [Quantity] int NOT NULL,
        CONSTRAINT [PK_Stock] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Stock_Catalog_CatalogItemId] FOREIGN KEY ([CatalogItemId]) REFERENCES [Catalog] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_BasketItems_BasketId] ON [BasketItems] ([BasketId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_Baskets_BuyerAddress] ON [Baskets] ([BuyerAddress]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_Baskets_BuyerId] ON [Baskets] ([BuyerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_BuyerPostalAddress_BuyerId] ON [BuyerPostalAddress] ([BuyerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Buyers_BuyerAddress] ON [Buyers] ([BuyerAddress]) WHERE [BuyerAddress] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Buyers_BuyerId] ON [Buyers] ([BuyerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_Catalog_CatalogBrandId] ON [Catalog] ([CatalogBrandId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_Catalog_CatalogTypeId] ON [Catalog] ([CatalogTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_Orders_BuyerAddress] ON [Orders] ([BuyerAddress]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_Orders_BuyerId] ON [Orders] ([BuyerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_QuoteItems_QuoteId] ON [QuoteItems] ([QuoteId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_Quotes_BuyerAddress] ON [Quotes] ([BuyerAddress]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_Quotes_BuyerId] ON [Quotes] ([BuyerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    CREATE INDEX [IX_Stock_CatalogItemId] ON [Stock] ([CatalogItemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311151648_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200311151648_InitialCreate', N'3.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311172359_BuyerWalletAddress')
BEGIN
    ALTER TABLE [Buyers] ADD [BuyerWalletAddress] nvarchar(43) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311172359_BuyerWalletAddress')
BEGIN
    CREATE UNIQUE INDEX [IX_Buyers_BuyerWalletAddress] ON [Buyers] ([BuyerWalletAddress]) WHERE [BuyerWalletAddress] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200311172359_BuyerWalletAddress')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200311172359_BuyerWalletAddress', N'3.1.2');
END;

GO

