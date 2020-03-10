using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.Infrastructure.Identity;

namespace Nethereum.eShop.InMemory.Infrastructure.Data.Config
{
    public class InMemoryEShopIdentityDbBootrapper : IEShopIdentityDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("Identity"));
        }
    }
}
