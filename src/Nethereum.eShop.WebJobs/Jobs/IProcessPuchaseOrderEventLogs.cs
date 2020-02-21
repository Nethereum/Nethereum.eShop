using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs.Jobs
{
    public interface IProcessPuchaseOrderEventLogs
    {
        Task ExecuteAsync(ILogger logger);
    }


}
