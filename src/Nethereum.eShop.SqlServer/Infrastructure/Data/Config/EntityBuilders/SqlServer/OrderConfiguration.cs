using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities.OrderAggregate;
using b = Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer
{
    public class OrderConfiguration : b.OrderConfiguration
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
        }
    }
}
