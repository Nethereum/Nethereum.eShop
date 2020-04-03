﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;

namespace Nethereum.eShop.EntityFramework.Catalog.EntityBuilders
{
    public class QuoteItemConfiguration : IEntityTypeConfiguration<QuoteItem>
    {
        public virtual void Configure(EntityTypeBuilder<QuoteItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, io => io.ConfigureCatalogItemExcerpt());

            builder.Property(oi => oi.UnitPrice)
                .IsPrice()
                .IsRequired();
        }
    }
}
