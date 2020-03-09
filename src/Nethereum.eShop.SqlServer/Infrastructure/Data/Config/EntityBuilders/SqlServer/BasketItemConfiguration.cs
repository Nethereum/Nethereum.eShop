using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.BasketAggregate;
using b = Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer
{
    public class BasketItemConfiguration : b.BasketItemConfiguration
    {
        public override void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            base.Configure(builder);
        }
    }
}
