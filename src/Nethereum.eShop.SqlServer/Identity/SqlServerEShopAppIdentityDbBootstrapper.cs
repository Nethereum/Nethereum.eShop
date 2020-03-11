using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.EntityFramework.Identity;

namespace Nethereum.eShop.SqlServer.Identity
{
    public class SqlServerEShopAppIdentityDbBootstrapper : EShopAppIdentityDbBootstrapperBase, IEShopIdentityDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext, SqlServerAppIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection_SqlServer")));
        }
    }
}
