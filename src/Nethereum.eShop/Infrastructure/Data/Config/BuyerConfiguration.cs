using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Buyer.PostalAddresses));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasIndex(b => b.BuyerId).IsUnique();
        }
    }
}
