using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders
{
    public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
    {
        public virtual void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               //.UseHiLo("catalog_type_hilo")
               .IsRequired();

            builder.Property(cb => cb.Type)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
