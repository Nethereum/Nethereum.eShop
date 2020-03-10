using Microsoft.Extensions.Configuration;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.InMemory.Infrastructure.Data.Config;
using Nethereum.eShop.Sqlite.Infrastructure.Data.Config;
using Nethereum.eShop.SqlServer.Infrastructure.Data.Config;

namespace Nethereum.eShop.DbFactory
{
    public static class EShopDbBootstrapper
    {
        public static IEShopDbBootstrapper CreateInMemoryDbBootstrapper() => new InMemoryEShopDbBootrapper();
        public static IEShopIdentityDbBootstrapper CreateInMemoryAppIdentityDbBootstrapper() => new InMemoryEShopAppIdentityDbBootrapper();

        public static IEShopDbBootstrapper CreateDbBootstrapper(IConfiguration configuration)
        {
            var name = configuration["CatalogDbProvider"]?.ToLower();
            return name switch
            {
                "sqlserver" => new SqlServerEShopDbBootstrapper(),
                "sqlite" => new SqliteEShopDbBootstrapper(),
                _ => new InMemoryEShopDbBootrapper()
            };
        }

        public static IEShopIdentityDbBootstrapper CreateAppIdentityDbBootstrapper(IConfiguration configuration)
        {
            var name = configuration["CatalogDbProvider"]?.ToLower();
            return name switch
            {
                "sqlserver" => new SqlServerEShopAppIdentityDbBootstrapper(),
                "sqlite" => new SqliteEShopAppIdentityDbBootstrapper(),
                _ => new InMemoryEShopAppIdentityDbBootrapper()
            };
        }
    }
}
