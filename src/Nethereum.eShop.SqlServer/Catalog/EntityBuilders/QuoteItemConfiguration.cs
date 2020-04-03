using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using b = Nethereum.eShop.EntityFramework.Catalog.EntityBuilders;

namespace Nethereum.eShop.SqlServer.Catalog.EntityBuilders
{
    public class QuoteItemConfiguration : b.QuoteItemConfiguration
    {
        public override void Configure(EntityTypeBuilder<QuoteItem> builder)
        {
            base.Configure(builder);
        }
    }
}
