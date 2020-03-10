using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.Infrastructure.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.Sqlite.Infrastructure.Data.Config
{
    public class SqliteEShopAppIdentityDbBootstrapper : IEShopIdentityDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            SqlMapper.RemoveTypeMap(typeof(DateTimeOffset));
            SqlMapper.AddTypeHandler(DateTimeOffsetHandler.Instance);

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("IdentityConnection")));
        }

        public Task EnsureCreatedAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
        {
            var context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            return context.Database.MigrateAsync(cancellationToken);
        }
    }
}
