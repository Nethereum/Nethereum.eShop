using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nethereum.eShop.ApplicationCore.Entities;
using b = Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders;

namespace Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer
{
    public class CatalogBrandConfiguration : b.CatalogBrandConfiguration
    {
        public override void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            base.Configure(builder);
            builder.Property(ci => ci.Id)
               .UseHiLo("catalog_brand_hilo");
        }
    }
}
