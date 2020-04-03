using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;
using b = Nethereum.eShop.EntityFramework.Catalog.EntityBuilders;

namespace Nethereum.eShop.SqlServer.Catalog.EntityBuilders
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
