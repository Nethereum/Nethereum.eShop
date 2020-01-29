using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.PurchaseOrderAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class PurchaseOrderRequestItemConfiguration : IEntityTypeConfiguration<PurchaseOrderRequestItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderRequestItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, io =>
            {
                io.WithOwner();

                io.Property(cio => cio.ProductName)
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.Property(oi => oi.UnitPrice)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
        }
    }
}
