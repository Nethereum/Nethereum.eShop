using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(b => b.BuyerAddress)
                .IsAddress()
                .IsRequired();

            builder.Property(b => b.BuyerId)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(b => b.BuyerId);
            builder.HasIndex(b => b.BuyerAddress);

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
