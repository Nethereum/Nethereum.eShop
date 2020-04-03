using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Interfaces
{
    public interface IContractDeploymentService
    {
        Task EnsureDeployedAsync(ILoggerFactory loggerFactory, CancellationToken cancellationToken = default);
    }
}
