﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;

namespace Nethereum.eShop.EntityFramework.Catalog.EntityBuilders
{
    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public virtual void Configure(EntityTypeBuilder<Quote> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Quote.QuoteItems));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(o => o.ShipTo, a => a.ConfigureAddress());
            builder.OwnsOne(o => o.BillTo, a => a.ConfigureAddress());
            builder.Property(o => o.BuyerId).HasMaxLength(256).IsRequired();
            builder.Property(o => o.BuyerAddress).IsAddress();
            builder.Property(o => o.ApproverAddress).IsAddress();
            builder.Property(o => o.BuyerWalletAddress).IsAddress();
            builder.Property(o => o.TransactionHash).IsHash();
            builder.Property(o => o.CurrencyAddress).IsAddress();
            builder.Property(o => o.CurrencySymbol).IsBytes32();
            builder.Property(o => o.SellerId).IsBytes32();
            builder.HasIndex(b => b.BuyerId);
            builder.HasIndex(b => b.BuyerAddress);
        }
    }
}
