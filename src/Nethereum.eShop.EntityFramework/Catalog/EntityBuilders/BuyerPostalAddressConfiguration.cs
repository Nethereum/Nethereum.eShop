﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate;

namespace Nethereum.eShop.EntityFramework.Catalog.EntityBuilders
{
    public class BuyerPostalAddressConfiguration : IEntityTypeConfiguration<BuyerPostalAddress>
    {
        public virtual void Configure(EntityTypeBuilder<BuyerPostalAddress> builder)
        {
            builder.OwnsOne(o => o.PostalAddress, a => a.ConfigureAddress());
        }
    }
}
