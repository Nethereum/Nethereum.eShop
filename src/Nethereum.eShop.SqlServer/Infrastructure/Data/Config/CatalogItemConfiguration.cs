using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");

            builder.Property(ci => ci.Id)
                .UseHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Gtin)
                .IsRequired(true)
                .HasMaxLength(14);

            builder.Property(ci => ci.Price)
                .IsRequired(true)
                .IsPrice();

            builder.Property(ci => ci.PictureUri)
                .IsUri()
                .IsRequired(false);

            builder.Property(ci => ci.PictureSmallUri)
                .IsUri()
                .IsRequired(false);

            builder.Property(ci => ci.PictureMediumUri)
                .IsUri()
                .IsRequired(false);

            builder.Property(ci => ci.PictureLargeUri)
                .IsUri()
                .IsRequired(false);

            builder.HasOne(ci => ci.CatalogBrand)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogBrandId);

            builder.HasOne(ci => ci.CatalogType)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogTypeId);

            builder.Property(ci => ci.Unit)
                .HasMaxLength(8);
        }
    }
}
