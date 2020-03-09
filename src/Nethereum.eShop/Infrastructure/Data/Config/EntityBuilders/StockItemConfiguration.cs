using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders
{
    public class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
    {
        public virtual void Configure(EntityTypeBuilder<StockItem> builder)
        {
            builder.ToTable("Stock");

            builder.Property(ci => ci.Id)
                // .UseHiLo("stock_hilo")
                .IsRequired();

            builder.Property(ci => ci.Location)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.HasOne(ci => ci.CatalogItem)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogItemId);
        }
    }
}
