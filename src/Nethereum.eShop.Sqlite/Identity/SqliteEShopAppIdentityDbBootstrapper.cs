using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.EntityFramework.Identity;
using Nethereum.eShop.Sqlite.Common.Dapper.TypeHandlers;
using System;

namespace Nethereum.eShop.Sqlite.Identity
{
    public class SqliteEShopAppIdentityDbBootstrapper : EShopAppIdentityDbBootstrapperBase, IEShopIdentityDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
            SqlMapper.AddTypeHandler(DateTimeOffsetHandler.Instance);

            services.AddDbContext<AppIdentityDbContext, SqliteAppIdentityDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("IdentityConnection_Sqlite")));
        }
    }
}
