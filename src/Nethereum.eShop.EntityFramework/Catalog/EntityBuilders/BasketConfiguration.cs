using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;

namespace Nethereum.eShop.EntityFramework.Catalog.EntityBuilders
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public virtual void Configure(EntityTypeBuilder<Basket> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(b => b.BuyerAddress)
                .IsAddress()
                .IsRequired();

            builder.Property(b => b.BuyerId)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(b => b.TransactionHash).IsHash();
            builder.OwnsOne(o => o.ShipTo, a => a.ConfigureAddress());
            builder.OwnsOne(o => o.BillTo, a => a.ConfigureAddress());


            builder.HasIndex(b => b.BuyerId);
            builder.HasIndex(b => b.BuyerAddress);
        }
    }
}
