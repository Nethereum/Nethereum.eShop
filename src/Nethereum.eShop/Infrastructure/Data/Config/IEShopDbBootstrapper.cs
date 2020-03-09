using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nethereum.eShop.Infrastructure.Data.Config
{
    public interface IEShopDbBootstrapper
    {
        void AddDbContext(IServiceCollection services, IConfiguration configuration);
        void AddRepositories(IServiceCollection services, IConfiguration configuration);

        void AddQueries(IServiceCollection services, IConfiguration configuration);
    }

    public interface IEShopIdentityDbBootstrapper
    {
        void AddDbContext(IServiceCollection services, IConfiguration configuration);
    }
}
