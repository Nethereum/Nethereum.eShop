using Microsoft.Extensions.Configuration;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.InMemory.Catalog;
using Nethereum.eShop.InMemory.Infrastructure.Identity;
using Nethereum.eShop.Sqlite.Catalog;
using Nethereum.eShop.Sqlite.Identity;
using Nethereum.eShop.SqlServer.Catalog;
using Nethereum.eShop.SqlServer.Identity;

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
            var name = configuration["AppIdentityDbProvider"]?.ToLower();
            return name switch
            {
                "sqlserver" => new SqlServerEShopAppIdentityDbBootstrapper(),
                "sqlite" => new SqliteEShopAppIdentityDbBootstrapper(),
                _ => new InMemoryEShopAppIdentityDbBootrapper()
            };
        }
    }
}
