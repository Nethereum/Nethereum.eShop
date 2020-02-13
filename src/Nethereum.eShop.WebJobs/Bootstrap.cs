namespace Nethereum.eShop.WebJobs
{
    public class Bootstrap
    {
        //public static BlockchainRegistryProcessor InitialiseBlockchainRegistryProcessor(TextWriter log,
        //    ICollector<RegistrationMessage> workRegisteredUnregisteredQueue, Web3 web3, AzureTable workRegistryTable)
        //{
        //    var service = new RegistryService(web3, ConfigurationSettings.GetWorkRegistryContractAddress());

        //    var workByteCodeMatcher = new ByteCodeMatcher(web3, new WorkContractDefinition());
        //    var logger = new TextWriterLogger(log);
        //    var storageService = new WorkRegistryRepository(workRegistryTable);
        //    var registryQueue = new QueueRegistryProcessorService(new QueueRegistry(workRegisteredUnregisteredQueue));
        //    var processingServices = new IRegistryProcessingService[] { storageService, registryQueue };

        //    var blockchainRegistryProcessor = new BlockchainRegistryProcessor(web3, service, logger, processingServices,
        //        workByteCodeMatcher);
        //    return blockchainRegistryProcessor;
        //}

        //public static BlokchainBatchProcessorService InitialiseBatchProcessorService(BlockchainRegistryProcessor blockchainRegistryProcessor,
        //   CloudTable workProcessRegistryTable,
        //   TextWriter logWriter, Web3 web3)
        //{
        //    var workRegistryProcessInfoRepository = new ProcessInfoRepository(workProcessRegistryTable);
        //    var logger = new TextWriterLogger(logWriter);
        //    var latestBlockchainProcessorService = new LatestBlockBlockchainProcessProgressService(web3,
        //        ConfigurationSettings.StartProcessFromBlockNumber(), workRegistryProcessInfoRepository
        //        );
        //    return new BlokchainBatchProcessorService(blockchainRegistryProcessor, logger, latestBlockchainProcessorService,
        //        200);
        //}
    }
}
