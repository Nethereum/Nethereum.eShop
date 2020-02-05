using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Nethereum.eShop.Infrastructure.Data
{
    public interface ICatalogContextSeeder
    {
        Task SeedAsync(CatalogContext catalogContext, ILoggerFactory loggerFactory, int? retry = 0);
    }
}
