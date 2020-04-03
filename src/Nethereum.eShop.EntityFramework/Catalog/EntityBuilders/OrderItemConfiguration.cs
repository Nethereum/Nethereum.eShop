using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;

namespace Nethereum.eShop.EntityFramework.Catalog.EntityBuilders
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public virtual void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, io => io.ConfigureCatalogItemExcerpt());

            builder.Property(oi => oi.UnitPrice)
                .IsRequired(true)
                .IsPrice();

            builder.Property(i => i.Unit).HasMaxLength(50);
            builder.Property(i => i.QuantityAddress).IsAddress();
            builder.Property(i => i.QuantitySymbol).IsBytes32();
            builder.Property(i => i.CurrencyValue).IsBigInteger();
        }
    }
}
