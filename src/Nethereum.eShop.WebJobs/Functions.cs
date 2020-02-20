using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Nethereum.eShop.WebJobs.Jobs;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs
{
    public class Functions
    {
        private readonly IProcessEventLogs _processEventLogs;
        private readonly ICreateFakePurchaseOrders _ceateFakePurchaseOrders;

        public Functions(IProcessEventLogs processEventLogs, ICreateFakePurchaseOrders createFakePurchaseOrders)
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

        static bool _processing = false;

        [Singleton]
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
