using Common.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nethereum.BlockchainProcessing;
using Nethereum.BlockchainProcessing.LogProcessing;
using Nethereum.BlockchainProcessing.Orchestrator;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.eShop.ApplicationCore.Exceptions;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.Microsoft.Logging.Utils;
using Nethereum.RPC.Eth.Blocks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Utils;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs.Jobs
{
    public class ProcessPurchaseOrderEventLogs : IProcessPuchaseOrderEventLogs
    {
        private readonly IConfiguration _configuration;
        private readonly ISettingRepository _settingRepository;
        private readonly IOrderService _orderService;
        private readonly IBlockProgressRepository BlockProgressRepository = null;

        public ProcessPurchaseOrderEventLogs(
            IConfiguration configuration,
            ISettingRepository settingRepository, 
            IOrderService orderService,
            IBlockProgressRepository blockProgressRepository
            )
        {
            _configuration = configuration;
            _settingRepository = settingRepository;
            _orderService = orderService;
            BlockProgressRepository = blockProgressRepository;
        }

        public async Task ExecuteAsync(ILogger logger)
        {
            var dbConfigSettings = await _settingRepository.GetEShopConfigurationSettingsAsync();

            if (!dbConfigSettings.ProcessPurchaseOrderEvents.Enabled)
            {
                logger.LogInformation($"{nameof(ProcessPurchaseOrderEventLogs)} is not enabled - see app settings");
                return;
            }

            const int RequestRetryWeight = 0; // see below for retry algorithm

            var url = _configuration["EthereumRpcUrl"];

            var web3 = new Web3.Web3(url);
            var filter = new NewFilterInput { Address = new[] { dbConfigSettings.PurchasingContractAddress } };

            ILog log = logger.ToILog();

            EventLogProcessorHandler<PurchaseOrderCreatedLogEventDTO> poCreatedHandler = 
                CreatePurchaseOrderCreatedHandler(logger);

            var logProcessorHandlers = new ProcessorHandler<FilterLog>[]
                {poCreatedHandler};

            IBlockchainProcessingOrchestrator orchestrator = new LogOrchestrator(
                ethApi: web3.Eth,
                logProcessors: logProcessorHandlers,
                filterInput: filter,
                defaultNumberOfBlocksPerRequest: dbConfigSettings.ProcessPurchaseOrderEvents.NumberOfBlocksPerBatch,
                retryWeight: RequestRetryWeight);

            IWaitStrategy waitForBlockConfirmationsStrategy = new WaitStrategy();

            ILastConfirmedBlockNumberService lastConfirmedBlockNumberService =
                new LastConfirmedBlockNumberService(
                    web3.Eth.Blocks.GetBlockNumber,
                    waitForBlockConfirmationsStrategy,
                    (uint)dbConfigSettings.ProcessPurchaseOrderEvents.MinimumBlockConfirmations,
                    log);

            var processor = new BlockchainProcessor(
                orchestrator, BlockProgressRepository, lastConfirmedBlockNumberService);

            var cancellationToken = new CancellationTokenSource(dbConfigSettings.ProcessPurchaseOrderEvents.TimeoutMs);

            var currentBlockOnChain = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            var blockToProcessTo = currentBlockOnChain.Value - dbConfigSettings.ProcessPurchaseOrderEvents.MinimumBlockConfirmations;
            var lastBlockProcessed = await BlockProgressRepository.GetLastBlockNumberProcessedAsync();
            var minStartingBlock = dbConfigSettings.ProcessPurchaseOrderEvents.MinimumStartingBlock;

            logger.LogInformation(
                $"Processing logs. To Block: {blockToProcessTo},  Last Block Processed: {lastBlockProcessed ?? 0}, Min Block: {minStartingBlock}");

            await processor.ExecuteAsync(
                toBlockNumber: blockToProcessTo,
                cancellationToken: cancellationToken.Token,
                startAtBlockNumberIfNotProcessed: minStartingBlock);
        }

        private EventLogProcessorHandler<PurchaseOrderCreatedLogEventDTO> CreatePurchaseOrderCreatedHandler(ILogger logger)
        {
            return new EventLogProcessorHandler<PurchaseOrderCreatedLogEventDTO>(
                action: async (log) =>
                {
                    logger.LogInformation(
    $"PurchaseOrderCreated: Block: {log.Log.BlockNumber}, PO Number: {log.Event.PoNumber}, QuoteId: {log.Event.Po.QuoteId}");

                    try
                    {
                        await _orderService.CreateOrderAsync(log.Log.TransactionHash, log.Event.Po);
                    }
                    catch (QuoteNotFoundException)
                    {
                        logger.LogError($"Quote: {log.Event.Po.QuoteId} could not be found");
                    }
                });
        }
    }


}
