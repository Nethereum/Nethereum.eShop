using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders
{
    public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public virtual void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               // .UseHiLo("catalog_brand_hilo")
               .IsRequired();

            builder.Property(cb => cb.Brand)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
