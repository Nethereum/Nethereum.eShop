CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `Baskets` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `BuyerId` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
        `BuyerAddress` varchar(43) CHARACTER SET utf8mb4 NOT NULL,
        `BillTo_RecipientName` varchar(255) CHARACTER SET utf8mb4 NULL,
        `BillTo_Street` varchar(180) CHARACTER SET utf8mb4 NULL,
        `BillTo_City` varchar(100) CHARACTER SET utf8mb4 NULL,
        `BillTo_State` varchar(60) CHARACTER SET utf8mb4 NULL,
        `BillTo_Country` varchar(90) CHARACTER SET utf8mb4 NULL,
        `BillTo_ZipCode` varchar(18) CHARACTER SET utf8mb4 NULL,
        `ShipTo_RecipientName` varchar(255) CHARACTER SET utf8mb4 NULL,
        `ShipTo_Street` varchar(180) CHARACTER SET utf8mb4 NULL,
        `ShipTo_City` varchar(100) CHARACTER SET utf8mb4 NULL,
        `ShipTo_State` varchar(60) CHARACTER SET utf8mb4 NULL,
        `ShipTo_Country` varchar(90) CHARACTER SET utf8mb4 NULL,
        `ShipTo_ZipCode` varchar(18) CHARACTER SET utf8mb4 NULL,
        `TransactionHash` varchar(67) CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Baskets` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `Buyers` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `BuyerId` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
        `BuyerAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `BuyerWalletAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Buyers` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `CatalogBrands` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Brand` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_CatalogBrands` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `CatalogTypes` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Type` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_CatalogTypes` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `Orders` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `QuoteId` int NULL,
        `Status` int NOT NULL,
        `TransactionHash` varchar(67) CHARACTER SET utf8mb4 NULL,
        `BuyerId` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
        `BuyerAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `CurrencyAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `CurrencySymbol` varchar(32) CHARACTER SET utf8mb4 NULL,
        `ApproverAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `PoNumber` bigint NULL,
        `PoType` int NOT NULL,
        `BuyerWalletAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `SellerId` varchar(32) CHARACTER SET utf8mb4 NULL,
        `PoDate` datetime(6) NULL,
        `OrderDate` datetime(6) NOT NULL,
        `BillTo_RecipientName` varchar(255) CHARACTER SET utf8mb4 NULL,
        `BillTo_Street` varchar(180) CHARACTER SET utf8mb4 NULL,
        `BillTo_City` varchar(100) CHARACTER SET utf8mb4 NULL,
        `BillTo_State` varchar(60) CHARACTER SET utf8mb4 NULL,
        `BillTo_Country` varchar(90) CHARACTER SET utf8mb4 NULL,
        `BillTo_ZipCode` varchar(18) CHARACTER SET utf8mb4 NULL,
        `ShipTo_RecipientName` varchar(255) CHARACTER SET utf8mb4 NULL,
        `ShipTo_Street` varchar(180) CHARACTER SET utf8mb4 NULL,
        `ShipTo_City` varchar(100) CHARACTER SET utf8mb4 NULL,
        `ShipTo_State` varchar(60) CHARACTER SET utf8mb4 NULL,
        `ShipTo_Country` varchar(90) CHARACTER SET utf8mb4 NULL,
        `ShipTo_ZipCode` varchar(18) CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Orders` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `Quotes` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Status` int NOT NULL,
        `Date` datetime(6) NOT NULL,
        `Expiry` datetime(6) NOT NULL,
        `TransactionHash` varchar(67) CHARACTER SET utf8mb4 NULL,
        `BuyerAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `CurrencySymbol` varchar(32) CHARACTER SET utf8mb4 NULL,
        `CurrencyAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `ApproverAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `PoNumber` bigint NULL,
        `PoType` int NOT NULL,
        `BuyerWalletAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `SellerId` varchar(32) CHARACTER SET utf8mb4 NULL,
        `BuyerId` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
        `BillTo_RecipientName` varchar(255) CHARACTER SET utf8mb4 NULL,
        `BillTo_Street` varchar(180) CHARACTER SET utf8mb4 NULL,
        `BillTo_City` varchar(100) CHARACTER SET utf8mb4 NULL,
        `BillTo_State` varchar(60) CHARACTER SET utf8mb4 NULL,
        `BillTo_Country` varchar(90) CHARACTER SET utf8mb4 NULL,
        `BillTo_ZipCode` varchar(18) CHARACTER SET utf8mb4 NULL,
        `ShipTo_RecipientName` varchar(255) CHARACTER SET utf8mb4 NULL,
        `ShipTo_Street` varchar(180) CHARACTER SET utf8mb4 NULL,
        `ShipTo_City` varchar(100) CHARACTER SET utf8mb4 NULL,
        `ShipTo_State` varchar(60) CHARACTER SET utf8mb4 NULL,
        `ShipTo_Country` varchar(90) CHARACTER SET utf8mb4 NULL,
        `ShipTo_ZipCode` varchar(18) CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Quotes` PRIMARY KEY (`Id`)
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `BasketItems` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UnitPrice` decimal(18,2) NOT NULL,
        `Quantity` int NOT NULL,
        `CatalogItemId` int NOT NULL,
        `BasketId` int NOT NULL,
        CONSTRAINT `PK_BasketItems` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_BasketItems_Baskets_BasketId` FOREIGN KEY (`BasketId`) REFERENCES `Baskets` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `BuyerPostalAddress` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Name` longtext CHARACTER SET utf8mb4 NULL,
        `PostalAddress_RecipientName` varchar(255) CHARACTER SET utf8mb4 NULL,
        `PostalAddress_Street` varchar(180) CHARACTER SET utf8mb4 NULL,
        `PostalAddress_City` varchar(100) CHARACTER SET utf8mb4 NULL,
        `PostalAddress_State` varchar(60) CHARACTER SET utf8mb4 NULL,
        `PostalAddress_Country` varchar(90) CHARACTER SET utf8mb4 NULL,
        `PostalAddress_ZipCode` varchar(18) CHARACTER SET utf8mb4 NULL,
        `BuyerId` int NULL,
        CONSTRAINT `PK_BuyerPostalAddress` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_BuyerPostalAddress_Buyers_BuyerId` FOREIGN KEY (`BuyerId`) REFERENCES `Buyers` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `Catalog` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Status` int NOT NULL,
        `Gtin` varchar(14) CHARACTER SET utf8mb4 NOT NULL,
        `GtinRegistryId` int NULL,
        `Name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `Description` longtext CHARACTER SET utf8mb4 NULL,
        `Price` decimal(18,2) NOT NULL,
        `Unit` varchar(8) CHARACTER SET utf8mb4 NULL,
        `CatalogTypeId` int NOT NULL,
        `CatalogBrandId` int NOT NULL,
        `PictureUri` varchar(512) CHARACTER SET utf8mb4 NULL,
        `PictureSmallUri` varchar(512) CHARACTER SET utf8mb4 NULL,
        `PictureMediumUri` varchar(512) CHARACTER SET utf8mb4 NULL,
        `PictureLargeUri` varchar(512) CHARACTER SET utf8mb4 NULL,
        `AttributeJson` longtext CHARACTER SET utf8mb4 NULL,
        `Rank` int NOT NULL,
        `Height` int NOT NULL,
        `Width` int NOT NULL,
        `Depth` int NOT NULL,
        `Weight` int NOT NULL,
        CONSTRAINT `PK_Catalog` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Catalog_CatalogBrands_CatalogBrandId` FOREIGN KEY (`CatalogBrandId`) REFERENCES `CatalogBrands` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_Catalog_CatalogTypes_CatalogTypeId` FOREIGN KEY (`CatalogTypeId`) REFERENCES `CatalogTypes` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `OrderItems` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Status` int NOT NULL,
        `ItemOrdered_CatalogItemId` int NULL,
        `ItemOrdered_Gtin` varchar(14) CHARACTER SET utf8mb4 NULL,
        `ItemOrdered_GtinRegistryId` int NULL,
        `ItemOrdered_ProductName` varchar(50) CHARACTER SET utf8mb4 NULL,
        `ItemOrdered_PictureUri` varchar(512) CHARACTER SET utf8mb4 NULL,
        `UnitPrice` decimal(18,2) NOT NULL,
        `Quantity` int NOT NULL,
        `Unit` varchar(50) CHARACTER SET utf8mb4 NULL,
        `PoItemStatus` int NULL,
        `PoItemNumber` int NULL,
        `GoodsIssueDate` datetime(6) NULL,
        `ActualEscrowReleaseDate` datetime(6) NULL,
        `PlannedEscrowReleaseDate` datetime(6) NULL,
        `IsEscrowReleased` tinyint(1) NULL,
        `QuantitySymbol` varchar(32) CHARACTER SET utf8mb4 NULL,
        `QuantityAddress` varchar(43) CHARACTER SET utf8mb4 NULL,
        `CurrencyValue` varchar(100) CHARACTER SET utf8mb4 NULL,
        `OrderId` int NULL,
        CONSTRAINT `PK_OrderItems` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_OrderItems_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `QuoteItems` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `ItemOrdered_CatalogItemId` int NULL,
        `ItemOrdered_Gtin` varchar(14) CHARACTER SET utf8mb4 NULL,
        `ItemOrdered_GtinRegistryId` int NULL,
        `ItemOrdered_ProductName` varchar(50) CHARACTER SET utf8mb4 NULL,
        `ItemOrdered_PictureUri` varchar(512) CHARACTER SET utf8mb4 NULL,
        `UnitPrice` decimal(18,2) NOT NULL,
        `Quantity` int NOT NULL,
        `Unit` longtext CHARACTER SET utf8mb4 NULL,
        `PoItemNumber` int NULL,
        `EscrowReleaseDate` datetime(6) NULL,
        `QuantitySymbol` longtext CHARACTER SET utf8mb4 NULL,
        `QuantityAddress` longtext CHARACTER SET utf8mb4 NULL,
        `CurrencyValue` longtext CHARACTER SET utf8mb4 NULL,
        `QuoteId` int NULL,
        CONSTRAINT `PK_QuoteItems` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_QuoteItems_Quotes_QuoteId` FOREIGN KEY (`QuoteId`) REFERENCES `Quotes` (`Id`) ON DELETE RESTRICT
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE TABLE `Stock` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `CatalogItemId` int NOT NULL,
        `Location` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
        `Quantity` int NOT NULL,
        CONSTRAINT `PK_Stock` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_Stock_Catalog_CatalogItemId` FOREIGN KEY (`CatalogItemId`) REFERENCES `Catalog` (`Id`) ON DELETE CASCADE
    );

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_BasketItems_BasketId` ON `BasketItems` (`BasketId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_Baskets_BuyerAddress` ON `Baskets` (`BuyerAddress`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_Baskets_BuyerId` ON `Baskets` (`BuyerId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_BuyerPostalAddress_BuyerId` ON `BuyerPostalAddress` (`BuyerId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE UNIQUE INDEX `IX_Buyers_BuyerAddress` ON `Buyers` (`BuyerAddress`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE UNIQUE INDEX `IX_Buyers_BuyerId` ON `Buyers` (`BuyerId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE UNIQUE INDEX `IX_Buyers_BuyerWalletAddress` ON `Buyers` (`BuyerWalletAddress`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_Catalog_CatalogBrandId` ON `Catalog` (`CatalogBrandId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_Catalog_CatalogTypeId` ON `Catalog` (`CatalogTypeId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_OrderItems_OrderId` ON `OrderItems` (`OrderId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_Orders_BuyerAddress` ON `Orders` (`BuyerAddress`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_Orders_BuyerId` ON `Orders` (`BuyerId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_QuoteItems_QuoteId` ON `QuoteItems` (`QuoteId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_Quotes_BuyerAddress` ON `Quotes` (`BuyerAddress`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_Quotes_BuyerId` ON `Quotes` (`BuyerId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    CREATE INDEX `IX_Stock_CatalogItemId` ON `Stock` (`CatalogItemId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200312152404_InitialCreate') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20200312152404_InitialCreate', '3.1.2');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200331114834_RemoveBuyerAndWalletAddress') THEN

    ALTER TABLE `Orders` DROP INDEX `IX_Orders_BuyerAddress`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200331114834_RemoveBuyerAndWalletAddress') THEN

    ALTER TABLE `Orders` DROP COLUMN `ApproverAddress`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200331114834_RemoveBuyerAndWalletAddress') THEN

    ALTER TABLE `Orders` DROP COLUMN `BuyerAddress`;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20200331114834_RemoveBuyerAndWalletAddress') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20200331114834_RemoveBuyerAndWalletAddress', '3.1.2');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

