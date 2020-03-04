using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class QuoteItemConfiguration : IEntityTypeConfiguration<QuoteItem>
    {
        public void Configure(EntityTypeBuilder<QuoteItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, io =>
            {
                io.ConfigureCatalogItemExcerpt();
            });

            builder.Property(oi => oi.UnitPrice)
                .IsPrice()
                .IsRequired();
        }
    }
}
