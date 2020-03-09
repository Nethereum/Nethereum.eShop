using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using b = Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer
{
    public class QuoteItemConfiguration : b.QuoteItemConfiguration
    {
        public override void Configure(EntityTypeBuilder<QuoteItem> builder)
        {
            base.Configure(builder);
        }
    }
}
