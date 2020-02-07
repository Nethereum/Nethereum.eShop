using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Quote.QuoteItems));

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

            builder.Property(o => o.BuyerAddress).IsAddress();
            builder.Property(o => o.ApproverAddress).IsAddress();
            builder.Property(o => o.BuyerWalletAddress).IsAddress();
            builder.Property(o => o.TransactionHash).IsHash();
        }
    }
}
