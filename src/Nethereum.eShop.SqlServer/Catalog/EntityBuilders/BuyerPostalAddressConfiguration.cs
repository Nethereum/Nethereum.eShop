using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BuyerAggregate;
using b = Nethereum.eShop.EntityFramework.Catalog.EntityBuilders;

namespace Nethereum.eShop.SqlServer.Catalog.EntityBuilders
{
    public class BuyerPostalAddressConfiguration : b.BuyerPostalAddressConfiguration
    {
        public override void Configure(EntityTypeBuilder<BuyerPostalAddress> builder)
        {
            base.Configure(builder);
        }
    }
}
