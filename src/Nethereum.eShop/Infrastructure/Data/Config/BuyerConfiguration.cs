using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.HasIndex(b => b.BuyerId).IsUnique();

            builder.OwnsOne(o => o.ShipTo, a =>
            {
                a.ConfigureAddress();
            });

            builder.OwnsOne(o => o.BillTo, a =>
            {
                a.ConfigureAddress();
            });
        }
    }
}
