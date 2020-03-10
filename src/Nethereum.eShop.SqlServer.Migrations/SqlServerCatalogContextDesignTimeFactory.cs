using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nethereum.eShop.Infrastructure.Data;
using Nethereum.eShop.Infrastructure.Data.Config.EntityBuilders.SqlServer;

namespace Nethereum.eShop.SqlServer.Infrastructure.Data.Config
{
    public class SqlServerCatalogContextDesignTimeFactory: IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost\\sqldev;Integrated Security=true;Initial Catalog=eShop;",
                b => b.MigrationsAssembly("Nethereum.eShop.SqlServer.Migrations"));

            return new CatalogContext(
                optionsBuilder.Options, 
                null, 
                new ModelBuilderAssemblyHandler<CatalogContext>(typeof(BasketConfiguration).Assembly));
        }
    }
}
