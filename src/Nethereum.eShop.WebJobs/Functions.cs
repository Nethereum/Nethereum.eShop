using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Nethereum.eShop.WebJobs.Jobs;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs
{
    public class Functions
    {
        private readonly IProcessPuchaseOrderEventLogs _processEventLogs;
        private readonly ICreateFakePurchaseOrders _ceateFakePurchaseOrders;

        public Functions(IProcessPuchaseOrderEventLogs processEventLogs, ICreateFakePurchaseOrders createFakePurchaseOrders)
        {
            _processEventLogs = processEventLogs;
            _ceateFakePurchaseOrders = createFakePurchaseOrders;
        }

        public void ProcessQueueMessage([QueueTrigger("webjob")] string message, ILogger logger)
        {
            logger.LogInformation(message);
        }

        public async Task CreateFakePurchaseOrders([TimerTrigger("00:00:05")] TimerInfo timer, ILogger logger)
        {
            logger.LogInformation("Start job CreateFakePurchaseOrders");
            await _ceateFakePurchaseOrders.ExecuteAsync(logger);
        }

        // TODO: Investigate how to prevent parallel execution of timed job
        // i.e. when the first job is taking longer than anticipated
        // the hack below works locally using the same app instance
        // but will most likely fail on Azure
        // where a new instance of the app will probably be instantiated on each interval
        static bool _processing = false;

        // [Singleton]
        public async Task ProcessBlockchainEvents([TimerTrigger("00:00:05")] TimerInfo timer, ILogger logger)
        {
            if (_processing == false)
            {
                _processing = true;
                try
                {
                    logger.LogInformation("Start job ProcessBlockchainEvents");
                    await _processEventLogs.ExecuteAsync(logger);
                }
                finally
                {
                    _processing = false;
                }
            }
        }

    }
}
