using Microsoft.Extensions.Logging;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.WebJobs.Configuration;
using Nethereum.Web3.Accounts;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs.Jobs
{
    public class ProcessPurchaseOrderEventLogs : IProcessPuchaseOrderEventLogs
    {
        private readonly EshopConfiguration eshopConfiguration;
        private readonly PurchaseOrderEventLogProcessingConfiguration config;

        public ProcessPurchaseOrderEventLogs(EshopConfiguration eshopConfiguration, IOrderService orderService)
        {
            this.eshopConfiguration = eshopConfiguration;
            OrderService = orderService;
            this.config = eshopConfiguration.PurchaseOrderEventLogConfiguration;
        }

        static JsonBlockProgressRepository BlockProgressRepository = null;

        public IOrderService OrderService { get; }

        public async Task ExecuteAsync(ILogger logger)
        {
            if (!config.Enabled) return;

            var account = new Account(eshopConfiguration.AccountPrivateKey);
            var web3 = new Web3.Web3(account, eshopConfiguration.EthereumRpcUrl);

            BlockProgressRepository = BlockProgressRepository ?? CreateBlockProgressRepository();

            // TODO: Configure processing properly
            // SQL backend for progress
            // Open up to handle many events

            var logProcessor = web3.Processing.Logs.CreateProcessorForContract<PurchaseOrderCreatedLogEventDTO>(
                config.PurchasingContractAddress, async log =>
                {
                    logger.LogInformation(
                        $"PurchaseOrderCreated: Block: {log.Log.BlockNumber}, PO Number: {log.Event.PoNumber}, QuoteId: {log.Event.Po.QuoteId}");

                    await OrderService.CreateOrderAsync(log.Log.TransactionHash, log.Event.Po);
                },
                minimumBlockConfirmations: config.MinimumBlockConfirmations,
                blockProgressRepository: BlockProgressRepository);

            var lastBlockProcessed = await BlockProgressRepository.GetLastBlockNumberProcessedAsync();

            BigInteger minStartingBlock = config.GetMinimumStartingBlock();

            if(lastBlockProcessed == null || lastBlockProcessed < minStartingBlock)
            {
                lastBlockProcessed = minStartingBlock == 0 ? 0 : minStartingBlock - 1;
            }

            BigInteger toBlock = (lastBlockProcessed.Value + config.NumberOfBlocksPerBatch);

            await logProcessor.ExecuteAsync(toBlockNumber: toBlock, startAtBlockNumberIfNotProcessed: minStartingBlock);
        }

        private JsonBlockProgressRepository CreateBlockProgressRepository()
        {
            return new JsonBlockProgressRepository(
                () => Task.FromResult(File.Exists(config.BlockProgressJsonFile)),
                async (json) => await File.WriteAllTextAsync(config.BlockProgressJsonFile, json),
                async () => await File.ReadAllTextAsync(config.BlockProgressJsonFile));
        }
    }


}
