using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IEShopIdentityDbBootstrapper
    {
        void AddDbContext(IServiceCollection services, IConfiguration configuration);
    }
}
