using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using b = Nethereum.eShop.EntityFramework.Catalog.EntityBuilders;

namespace Nethereum.eShop.SqlServer.Catalog.EntityBuilders
{
    public class BasketItemConfiguration : b.BasketItemConfiguration
    {
        public override void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            base.Configure(builder);
        }
    }
}
