using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IEShopDbBootstrapper
    {
        void AddDbContext(IServiceCollection services, IConfiguration configuration);
        void AddRepositories(IServiceCollection services, IConfiguration configuration);

        void AddQueries(IServiceCollection services, IConfiguration configuration);

        Task EnsureCreatedAsync(IServiceProvider serviceProvider);
    }
}
