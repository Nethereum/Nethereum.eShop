using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.EntityFramework.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.InMemory.Infrastructure.Identity
{
    public class InMemoryEShopAppIdentityDbBootrapper : EShopAppIdentityDbBootstrapperBase, IEShopIdentityDbBootstrapper
    {
        public void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext, InMemoryAppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("Identity"));
        }

        public override Task EnsureCreatedAsync(IServiceProvider serviceProvider, IConfiguration configuration, CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
