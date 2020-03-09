using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using b = Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer
{
    public class QuoteConfiguration : b.QuoteConfiguration
    {
        public override void Configure(EntityTypeBuilder<Quote> builder)
        {
            base.Configure(builder);
        }
    }
}
