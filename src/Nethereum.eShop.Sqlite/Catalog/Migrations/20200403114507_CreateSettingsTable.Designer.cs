﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nethereum.eShop.Sqlite.Catalog;

namespace Nethereum.eShop.Sqlite.Catalog.Migrations
{
    [DbContext(typeof(SqliteCatalogContext))]
    [Migration("20200403114507_CreateSettingsTable")]
    partial class CreateSettingsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.BasketAggregate.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BuyerAddress")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("TransactionHash")
                        .HasColumnType("TEXT")
                        .HasMaxLength(67);

                    b.HasKey("Id");

                    b.HasIndex("BuyerAddress");

                    b.HasIndex("BuyerId");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.BasketAggregate.BasketItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BasketId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BuyerAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("BuyerWalletAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.HasKey("Id");

                    b.HasIndex("BuyerAddress")
                        .IsUnique();

                    b.HasIndex("BuyerId")
                        .IsUnique();

                    b.HasIndex("BuyerWalletAddress")
                        .IsUnique();

                    b.ToTable("Buyers");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate.BuyerPostalAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BuyerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.ToTable("BuyerPostalAddress");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.CatalogBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("CatalogBrands");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AttributeJson")
                        .HasColumnType("TEXT");

                    b.Property<int>("CatalogBrandId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CatalogTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Depth")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Gtin")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(14);

                    b.Property<int?>("GtinRegistryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("PictureLargeUri")
                        .HasColumnType("TEXT")
                        .HasMaxLength(512);

                    b.Property<string>("PictureMediumUri")
                        .HasColumnType("TEXT")
                        .HasMaxLength(512);

                    b.Property<string>("PictureSmallUri")
                        .HasColumnType("TEXT")
                        .HasMaxLength(512);

                    b.Property<string>("PictureUri")
                        .HasColumnType("TEXT")
                        .HasMaxLength(512);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Unit")
                        .HasColumnType("TEXT")
                        .HasMaxLength(8);

                    b.Property<int>("Weight")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CatalogBrandId");

                    b.HasIndex("CatalogTypeId");

                    b.ToTable("Catalog");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.CatalogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("CatalogTypes");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Key");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("BuyerWalletAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.Property<string>("CurrencyAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.Property<string>("CurrencySymbol")
                        .HasColumnType("TEXT")
                        .HasMaxLength(32);

                    b.Property<DateTimeOffset>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("PoDate")
                        .HasColumnType("TEXT");

                    b.Property<long?>("PoNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PoType")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuoteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SellerId")
                        .HasColumnType("TEXT")
                        .HasMaxLength(32);

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TransactionHash")
                        .HasColumnType("TEXT")
                        .HasMaxLength(67);

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("ActualEscrowReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CurrencyValue")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<DateTimeOffset?>("GoodsIssueDate")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsEscrowReleased")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("PlannedEscrowReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PoItemNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PoItemStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuantityAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.Property<string>("QuantitySymbol")
                        .HasColumnType("TEXT")
                        .HasMaxLength(32);

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Unit")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApproverAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.Property<string>("BuyerAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("BuyerWalletAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.Property<string>("CurrencyAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(43);

                    b.Property<string>("CurrencySymbol")
                        .HasColumnType("TEXT")
                        .HasMaxLength(32);

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("Expiry")
                        .HasColumnType("TEXT");

                    b.Property<long?>("PoNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PoType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SellerId")
                        .HasColumnType("TEXT")
                        .HasMaxLength(32);

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TransactionHash")
                        .HasColumnType("TEXT")
                        .HasMaxLength(67);

                    b.HasKey("Id");

                    b.HasIndex("BuyerAddress");

                    b.HasIndex("BuyerId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate.QuoteItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CurrencyValue")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("EscrowReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PoItemNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuantityAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("QuantitySymbol")
                        .HasColumnType("TEXT");

                    b.Property<int?>("QuoteId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Unit")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("QuoteId");

                    b.ToTable("QuoteItems");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.StockItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CatalogItemId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.BasketAggregate.Basket", b =>
                {
                    b.OwnsOne("Nethereum.eShop.ApplicationCore.Entities.PostalAddress", "BillTo", b1 =>
                        {
                            b1.Property<int>("BasketId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(100);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(90);

                            b1.Property<string>("RecipientName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(255);

                            b1.Property<string>("State")
                                .HasColumnType("TEXT")
                                .HasMaxLength(60);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(180);

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(18);

                            b1.HasKey("BasketId");

                            b1.ToTable("Baskets");

                            b1.WithOwner()
                                .HasForeignKey("BasketId");
                        });

                    b.OwnsOne("Nethereum.eShop.ApplicationCore.Entities.PostalAddress", "ShipTo", b1 =>
                        {
                            b1.Property<int>("BasketId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(100);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(90);

                            b1.Property<string>("RecipientName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(255);

                            b1.Property<string>("State")
                                .HasColumnType("TEXT")
                                .HasMaxLength(60);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(180);

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(18);

                            b1.HasKey("BasketId");

                            b1.ToTable("Baskets");

                            b1.WithOwner()
                                .HasForeignKey("BasketId");
                        });
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.BasketAggregate.BasketItem", b =>
                {
                    b.HasOne("Nethereum.eShop.ApplicationCore.Entities.BasketAggregate.Basket", null)
                        .WithMany("Items")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate.BuyerPostalAddress", b =>
                {
                    b.HasOne("Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate.Buyer", null)
                        .WithMany("PostalAddresses")
                        .HasForeignKey("BuyerId");

                    b.OwnsOne("Nethereum.eShop.ApplicationCore.Entities.PostalAddress", "PostalAddress", b1 =>
                        {
                            b1.Property<int>("BuyerPostalAddressId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(100);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(90);

                            b1.Property<string>("RecipientName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(255);

                            b1.Property<string>("State")
                                .HasColumnType("TEXT")
                                .HasMaxLength(60);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(180);

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(18);

                            b1.HasKey("BuyerPostalAddressId");

                            b1.ToTable("BuyerPostalAddress");

                            b1.WithOwner()
                                .HasForeignKey("BuyerPostalAddressId");
                        });
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.CatalogItem", b =>
                {
                    b.HasOne("Nethereum.eShop.ApplicationCore.Entities.CatalogBrand", "CatalogBrand")
                        .WithMany()
                        .HasForeignKey("CatalogBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nethereum.eShop.ApplicationCore.Entities.CatalogType", "CatalogType")
                        .WithMany()
                        .HasForeignKey("CatalogTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.OrderAggregate.Order", b =>
                {
                    b.OwnsOne("Nethereum.eShop.ApplicationCore.Entities.PostalAddress", "BillTo", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(100);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(90);

                            b1.Property<string>("RecipientName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(255);

                            b1.Property<string>("State")
                                .HasColumnType("TEXT")
                                .HasMaxLength(60);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(180);

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(18);

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("Nethereum.eShop.ApplicationCore.Entities.PostalAddress", "ShipTo", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(100);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(90);

                            b1.Property<string>("RecipientName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(255);

                            b1.Property<string>("State")
                                .HasColumnType("TEXT")
                                .HasMaxLength(60);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(180);

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(18);

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("Nethereum.eShop.ApplicationCore.Entities.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.OwnsOne("Nethereum.eShop.ApplicationCore.Entities.CatalogItemExcerpt", "ItemOrdered", b1 =>
                        {
                            b1.Property<int>("OrderItemId")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("CatalogItemId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Gtin")
                                .HasColumnType("TEXT")
                                .HasMaxLength(14);

                            b1.Property<int?>("GtinRegistryId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("PictureUri")
                                .HasColumnType("TEXT")
                                .HasMaxLength(512);

                            b1.Property<string>("ProductName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(50);

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate.Quote", b =>
                {
                    b.OwnsOne("Nethereum.eShop.ApplicationCore.Entities.PostalAddress", "BillTo", b1 =>
                        {
                            b1.Property<int>("QuoteId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(100);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(90);

                            b1.Property<string>("RecipientName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(255);

                            b1.Property<string>("State")
                                .HasColumnType("TEXT")
                                .HasMaxLength(60);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(180);

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(18);

                            b1.HasKey("QuoteId");

                            b1.ToTable("Quotes");

                            b1.WithOwner()
                                .HasForeignKey("QuoteId");
                        });

                    b.OwnsOne("Nethereum.eShop.ApplicationCore.Entities.PostalAddress", "ShipTo", b1 =>
                        {
                            b1.Property<int>("QuoteId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(100);

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(90);

                            b1.Property<string>("RecipientName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(255);

                            b1.Property<string>("State")
                                .HasColumnType("TEXT")
                                .HasMaxLength(60);

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(180);

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(18);

                            b1.HasKey("QuoteId");

                            b1.ToTable("Quotes");

                            b1.WithOwner()
                                .HasForeignKey("QuoteId");
                        });
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate.QuoteItem", b =>
                {
                    b.HasOne("Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate.Quote", null)
                        .WithMany("QuoteItems")
                        .HasForeignKey("QuoteId");

                    b.OwnsOne("Nethereum.eShop.ApplicationCore.Entities.CatalogItemExcerpt", "ItemOrdered", b1 =>
                        {
                            b1.Property<int>("QuoteItemId")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("CatalogItemId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Gtin")
                                .HasColumnType("TEXT")
                                .HasMaxLength(14);

                            b1.Property<int?>("GtinRegistryId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("PictureUri")
                                .HasColumnType("TEXT")
                                .HasMaxLength(512);

                            b1.Property<string>("ProductName")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasMaxLength(50);

                            b1.HasKey("QuoteItemId");

                            b1.ToTable("QuoteItems");

                            b1.WithOwner()
                                .HasForeignKey("QuoteItemId");
                        });
                });

            modelBuilder.Entity("Nethereum.eShop.ApplicationCore.Entities.StockItem", b =>
                {
                    b.HasOne("Nethereum.eShop.ApplicationCore.Entities.CatalogItem", "CatalogItem")
                        .WithMany()
                        .HasForeignKey("CatalogItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
