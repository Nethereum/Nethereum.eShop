using Common.Logging;
using Microsoft.Extensions.Logging;
using Nethereum.BlockchainProcessing;
using Nethereum.BlockchainProcessing.LogProcessing;
using Nethereum.BlockchainProcessing.Orchestrator;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.Commerce.Contracts.BuyerWallet;
using Nethereum.Commerce.Contracts.Purchasing;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.eShop.ApplicationCore.Exceptions;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.WebJobs.Configuration;
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
        private readonly EshopConfiguration _eshopConfiguration;
        private readonly PurchaseOrderEventLogProcessingConfiguration _config;
        private readonly IOrderService _orderService;
        private readonly IBlockProgressRepository BlockProgressRepository = null;

        public ProcessPurchaseOrderEventLogs(
            EshopConfiguration eshopConfiguration, 
            IOrderService orderService,
            IBlockProgressRepository blockProgressRepository
            )
        {
            _eshopConfiguration = eshopConfiguration;
            _orderService = orderService;
            _config = eshopConfiguration.PurchaseOrderEventLogConfiguration;
            BlockProgressRepository = blockProgressRepository;
        }

        public async Task ExecuteAsync(ILogger logger)
        {
            if (!_config.Enabled)
            {
                logger.LogInformation($"{nameof(ProcessPurchaseOrderEventLogs)} is not enabled - see app settings");
                return;
            }

            const int RequestRetryWeight = 0; // see below for retry algorithm

            var web3 = new Web3.Web3(_eshopConfiguration.EthereumRpcUrl);
            var filter = new NewFilterInput { Address = new[] { _eshopConfiguration.PurchasingContractAddress } };

            ILog log = logger.ToILog();

            EventLogProcessorHandler<PurchaseOrderCreatedLogEventDTO> poCreatedHandler = 
                CreatePurchaseOrderCreatedHandler(logger);

            var logProcessorHandlers = new ProcessorHandler<FilterLog>[]
                {poCreatedHandler};

            IBlockchainProcessingOrchestrator orchestrator = new LogOrchestrator(
                ethApi: web3.Eth,
                logProcessors: logProcessorHandlers,
                filterInput: filter,
                defaultNumberOfBlocksPerRequest: (int)_config.NumberOfBlocksPerBatch,
                retryWeight: RequestRetryWeight);

            IWaitStrategy waitForBlockConfirmationsStrategy = new WaitStrategy();

            ILastConfirmedBlockNumberService lastConfirmedBlockNumberService =
                new LastConfirmedBlockNumberService(
                    web3.Eth.Blocks.GetBlockNumber,
                    waitForBlockConfirmationsStrategy,
                    _config.MinimumBlockConfirmations,
                    log);

            var processor = new BlockchainProcessor(
                orchestrator, BlockProgressRepository, lastConfirmedBlockNumberService);

            var cancellationToken = new CancellationTokenSource(_config.TimeoutMs);

            var currentBlockOnChain = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            var blockToProcessTo = currentBlockOnChain.Value - _config.MinimumBlockConfirmations;
            var lastBlockProcessed = await BlockProgressRepository.GetLastBlockNumberProcessedAsync();
            var minStartingBlock = _config.GetMinimumStartingBlock();

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
