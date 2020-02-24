using Microsoft.Extensions.Logging;
using Nethereum.BlockchainProcessing;
using Nethereum.BlockchainProcessing.LogProcessing;
using Nethereum.BlockchainProcessing.Orchestrator;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.eShop.ApplicationCore.Exceptions;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.WebJobs.Configuration;
using Nethereum.RPC.Eth.Blocks;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Utils;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.WebJobs.Jobs
{
    public class ProcessPurchaseOrderEventLogs : IProcessPuchaseOrderEventLogs
    {
        private readonly EshopConfiguration _eshopConfiguration;
        private readonly PurchaseOrderEventLogProcessingConfiguration _config;
        private readonly IOrderService _orderService;

        public ProcessPurchaseOrderEventLogs(EshopConfiguration eshopConfiguration, IOrderService orderService)
        {
            _eshopConfiguration = eshopConfiguration;
            _orderService = orderService;
            _config = eshopConfiguration.PurchaseOrderEventLogConfiguration;
        }

        static JsonBlockProgressRepository BlockProgressRepository = null;

        public async Task ExecuteAsync(ILogger logger)
        {
            if (!_config.Enabled)
            {
                logger.LogInformation($"{nameof(ProcessPurchaseOrderEventLogs)} is not enabled - see app settings");
                return;
            }

            const int RequestRetryWeight = 0; // see below for retry algorithm

            var web3 = new Web3.Web3(_eshopConfiguration.EthereumRpcUrl);
            var filter = new NewFilterInput{ Address = new[] { _config.PurchasingContractAddress } };

            var poCreatedHandler = new EventLogProcessorHandler<PurchaseOrderCreatedLogEventDTO>(
                action: async(log) =>
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

            IEnumerable<ProcessorHandler<FilterLog>> logProcessorHandlers = new ProcessorHandler<FilterLog>[]
                {poCreatedHandler};

            IBlockchainProcessingOrchestrator orchestrator = new LogOrchestrator(
                ethApi: web3.Eth,
                logProcessors: logProcessorHandlers,
                filterInput: filter,
                defaultNumberOfBlocksPerRequest: (int)_config.NumberOfBlocksPerBatch,
                retryWeight: RequestRetryWeight);

            // TODO: SQL backend for progress
            // Wire up to handle many events

            BlockProgressRepository = BlockProgressRepository ?? CreateBlockProgressRepository();

            IWaitStrategy waitForBlockConfirmationsStrategy = new WaitStrategy();

            ILastConfirmedBlockNumberService lastConfirmedBlockNumberService =
                new LastConfirmedBlockNumberService(
                    web3.Eth.Blocks.GetBlockNumber, waitForBlockConfirmationsStrategy, _config.MinimumBlockConfirmations);

            var processor = new BlockchainProcessor(
                orchestrator, BlockProgressRepository, lastConfirmedBlockNumberService);

            //TODO: Implement a timeout from config
            var cancellationToken = new CancellationTokenSource();

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


        private JsonBlockProgressRepository CreateBlockProgressRepository()
        {
            return new JsonBlockProgressRepository(
                () => Task.FromResult(File.Exists(_config.BlockProgressJsonFile)),
                async (json) => await File.WriteAllTextAsync(_config.BlockProgressJsonFile, json),
                async () => await File.ReadAllTextAsync(_config.BlockProgressJsonFile));
        }
    }


}
