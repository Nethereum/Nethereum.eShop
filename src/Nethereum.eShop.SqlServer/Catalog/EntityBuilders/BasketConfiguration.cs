using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using b = Nethereum.eShop.EntityFramework.Catalog.EntityBuilders;

namespace Nethereum.eShop.SqlServer.Catalog.EntityBuilders
{
    public class BasketConfiguration : b.BasketConfiguration
    {
        public override void Configure(EntityTypeBuilder<Basket> builder)
        {
            base.Configure(builder);
        }
    }
}
