using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using b = Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer
{
    public class BasketConfiguration : b.BasketConfiguration
    {
        public override void Configure(EntityTypeBuilder<Basket> builder)
        {
            base.Configure(builder);
        }
    }
}
