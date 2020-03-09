using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;
using b = Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer
{
    public class StockItemConfiguration : b.StockItemConfiguration
    {
        public override void Configure(EntityTypeBuilder<StockItem> builder)
        {
            base.Configure(builder);

            builder.Property(ci => ci.Id)
                .UseHiLo("stock_hilo");
        }
    }
}
