using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.EntityFramework.Identity;

namespace Nethereum.eShop.MySql.Identity
{
    public class MySqlEShopAppIdentityDbBootstrapper : EShopAppIdentityDbBootstrapperBase, IEShopIdentityDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext, MySqlAppIdentityDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("IdentityConnection_MySql")));
        }
    }
}
