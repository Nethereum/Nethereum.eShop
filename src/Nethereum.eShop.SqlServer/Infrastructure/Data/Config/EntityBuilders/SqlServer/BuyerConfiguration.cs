using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate;
using b = Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer
{
    public class BuyerConfiguration : b.BuyerConfiguration
    {
        public override void Configure(EntityTypeBuilder<Buyer> builder)
        {
            base.Configure(builder);
        }
    }
}
