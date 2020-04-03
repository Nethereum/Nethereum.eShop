using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IEShopIdentityDbBootstrapper
    {
        void AddDbContext(IServiceCollection services, IConfiguration configuration);

        void AddIdentityStores(IdentityBuilder identityBuilder,  IServiceCollection services);

        Task EnsureCreatedAsync(IServiceProvider serviceProvider, IConfiguration configuration, CancellationToken cancellationToken = default);
    }
}
