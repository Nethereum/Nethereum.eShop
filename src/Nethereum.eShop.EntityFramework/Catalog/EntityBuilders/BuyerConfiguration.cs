using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate;

namespace Nethereum.eShop.EntityFramework.Catalog.EntityBuilders
{
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public virtual void Configure(EntityTypeBuilder<Buyer> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Buyer.PostalAddresses));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(b => b.BuyerId).HasMaxLength(256).IsRequired();
            builder.Property(b => b.BuyerAddress).IsAddress();

            builder.HasIndex(b => b.BuyerId).IsUnique();
            builder.HasIndex(b => b.BuyerAddress).IsUnique();
        }
    }
}
