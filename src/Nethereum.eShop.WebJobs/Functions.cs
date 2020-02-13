using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("webjob")] string message, ILogger logger)
        {
            logger.LogInformation(message);
        }

        public static void ProcessQueueMessage2([QueueTrigger("webjob2")] string message, ILogger logger)
        {
            logger.LogInformation(message);
        }

        // [Singleton]
        public static async Task ProcessBlockchainEvents([TimerTrigger("00:00:05")] TimerInfo timer, ILogger logger)
        {
            logger.LogInformation("Start job ProcessBlockchainEvents");

            //var workRegistryTable = new AzureTable(tableBinding);

            //var web3 = new Web3(ConfigurationSettings.GetEthereumRPCUrl());

            //var blockchainRegistryProcessor = Bootstrap.InitialiseBlockchainRegistryProcessor(log, workRegisteredUnregisteredQueue, web3, workRegistryTable);
            //var batchProcessor = Bootstrap.InitialiseBatchProcessorService(blockchainRegistryProcessor,
            //    workRegistryTable, log, web3);

            //await batchProcessor.ProcessLatestBlocks();
        }
    }
}
