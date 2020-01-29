using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using Nethereum.eShop.ApplicationCore.Entities.PurchaseOrderAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class PurchaseOrderRequestConfiguration : IEntityTypeConfiguration<PurchaseOrderRequest>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderRequest> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(PurchaseOrderRequest.OrderItems));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasIndex(b => b.BuyerId);
            builder.HasIndex(b => b.BuyerNonce);

            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();
                
                a.Property(a => a.ZipCode)
                    .HasMaxLength(18)
                    .IsRequired();

                a.Property(a => a.Street)
                    .HasMaxLength(180)
                    .IsRequired();

                a.Property(a => a.State)
                    .HasMaxLength(60);

                a.Property(a => a.Country)
                    .HasMaxLength(90)
                    .IsRequired();

                a.Property(a => a.City)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            builder.OwnsOne(o => o.BillToAddress, a =>
            {
                a.WithOwner();

                a.Property(a => a.ZipCode)
                    .HasMaxLength(18)
                    .IsRequired();

                a.Property(a => a.Street)
                    .HasMaxLength(180)
                    .IsRequired();

                a.Property(a => a.State)
                    .HasMaxLength(60);

                a.Property(a => a.Country)
                    .HasMaxLength(90)
                    .IsRequired();

                a.Property(a => a.City)
                    .HasMaxLength(100)
                    .IsRequired();
            });
        }
    }
}
