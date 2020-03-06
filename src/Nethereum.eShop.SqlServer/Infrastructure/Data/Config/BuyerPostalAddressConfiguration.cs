using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class BuyerPostalAddressConfiguration : IEntityTypeConfiguration<BuyerPostalAddress>
    {
        public void Configure(EntityTypeBuilder<BuyerPostalAddress> builder)
        {
            builder.OwnsOne(o => o.PostalAddress, a =>
            {
                a.ConfigureAddress();
            });
        }
    }
}
