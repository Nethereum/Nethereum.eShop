using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using b = Nethereum.eShop.EntityFramework.Catalog.EntityBuilders;

namespace Nethereum.eShop.SqlServer.Catalog.EntityBuilders
{
    public class QuoteConfiguration : b.QuoteConfiguration
    {
        public override void Configure(EntityTypeBuilder<Quote> builder)
        {
            base.Configure(builder);
        }
    }
}
