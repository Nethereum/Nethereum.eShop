using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasIndex(b => b.BuyerAddress);

            builder.OwnsOne(o => o.ShipTo, a =>
            {
                a.ConfigureAddress();
            });

            builder.OwnsOne(o => o.BillTo, a =>
            {
                a.ConfigureAddress();
            });

            builder.Property(o => o.BuyerAddress).HasMaxLength(43);
        }

        /*
         * 
         *     public static class ColumnLengths
    {
        public const int AddressLength = 43;
        public const int HashLength = 67;
        public const int BigIntegerLength = 100;
    }
         */
    }
}
