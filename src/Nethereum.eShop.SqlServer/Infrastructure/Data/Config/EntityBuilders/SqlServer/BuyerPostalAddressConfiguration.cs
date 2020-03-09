using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate;
using b = Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer
{
    public class BuyerPostalAddressConfiguration : b.BuyerPostalAddressConfiguration
    {
        public override void Configure(EntityTypeBuilder<BuyerPostalAddress> builder)
        {
            base.Configure(builder);
        }
    }
}
