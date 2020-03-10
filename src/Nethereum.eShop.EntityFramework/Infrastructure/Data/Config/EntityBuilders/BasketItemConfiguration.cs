using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public virtual void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(bi => bi.UnitPrice)
                .IsRequired(true)
                .IsPrice();
        }
    }
}
