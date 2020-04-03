using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface ICatalogContextSeeder
    {
        Task SeedAsync(ILoggerFactory loggerFactory, int? retry = 0);
    }
}
