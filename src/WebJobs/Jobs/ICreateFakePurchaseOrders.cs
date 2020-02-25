using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs.Jobs
{
    public interface ICreateFakePurchaseOrders
    {
        Task ExecuteAsync(ILogger logger);
    }
}
