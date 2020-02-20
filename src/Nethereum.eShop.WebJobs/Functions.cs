using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Nethereum.ABI.Decoders;
using Nethereum.ABI.Encoders;
using Nethereum.Commerce.Contracts.Purchasing;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;
using static Nethereum.Commerce.Contracts.ContractEnums;

namespace Nethereum.eShop.WebJobs
{

    public class Functions
    {
        public IOrderService OrderService { get; }

        public Functions(IOrderService orderService)
        {
            OrderService = orderService;
        }

        public void ProcessQueueMessage([QueueTrigger("webjob")] string message, ILogger logger)
        {
            logger.LogInformation(message);
        }

        public void ProcessQueueMessage2([QueueTrigger("webjob2")] string message, ILogger logger)
        {
            logger.LogInformation(message);
        }

        // [Singleton]
        public async Task ProcessBlockchainEvents([TimerTrigger("00:00:05")] TimerInfo timer, ILogger logger)
        {
            logger.LogInformation("Start job ProcessBlockchainEvents");

            var account = new Account("0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0");
            var web3 = new Web3.Web3(account);

            var purchasingService = new PurchasingService(web3, "0xb897cca68ed5d6f56e6ed2bd127c2ebb5d0f192a");
            var isOwner = await purchasingService.IsOwnerQueryAsync();

            var bytes32Encoder = new Bytes32TypeEncoder();

            var latest = await purchasingService.GetPoNumberBySellerAndQuoteQueryAsync(new GetPoNumberBySellerAndQuoteFunction
            {
                SellerId = bytes32Encoder.Encode("Nethereum.eShop"),
                QuoteId = 1
            });

            
            var po = new CreatePurchaseOrderFunction
            {
                Po = new Po
                {
                    ApproverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                    BuyerAddress = "0x37ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                    ReceiverAddress = "0x36ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                    BuyerWalletAddress = "0xdaa19b96411d4837924cb57d3cf1c8e73b7fa92e",
                    PoCreateDate = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    QuoteId = 1,
                    PoType = (byte)PoType.Cash,
                    PoItemCount = 1,
                    SellerId = bytes32Encoder.Encode("Nethereum.eShop"),
                    PoItems = new System.Collections.Generic.List<PoItem>
                    {
                        new PoItem
                        {
                        PoItemNumber = 1,
                        SoNumber = bytes32Encoder.Encode("so1"),
                        SoItemNumber = bytes32Encoder.Encode("100"),
                        ProductId = bytes32Encoder.Encode("gtin1111"),
                        Quantity = 1,
                        Unit = bytes32Encoder.Encode("EA"),
                        QuantitySymbol = bytes32Encoder.Encode("NA"),
                        QuantityAddress = "0x40ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        CurrencyValue = 11,
                        CurrencySymbol = bytes32Encoder.Encode("DAI"),
                        CurrencyAddress = "0x41ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
                        Status = (byte)PoItemStatus.Created,
                        GoodsIssuedDate = 100,
                        GoodsReceivedDate = 0,
                        PlannedEscrowReleaseDate = 100,
                        ActualEscrowReleaseDate = 110,
                        IsEscrowReleased = false,
                        CancelStatus = (byte)PoItemCancelStatus.Initial
                        }
                    }
                }
            };

            var receipt = await purchasingService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(po);

            //var workRegistryTable = new AzureTable(tableBinding);

            //var web3 = new Web3(ConfigurationSettings.GetEthereumRPCUrl());

            //var blockchainRegistryProcessor = Bootstrap.InitialiseBlockchainRegistryProcessor(log, workRegisteredUnregisteredQueue, web3, workRegistryTable);
            //var batchProcessor = Bootstrap.InitialiseBatchProcessorService(blockchainRegistryProcessor,
            //    workRegistryTable, log, web3);

            //await batchProcessor.ProcessLatestBlocks();
        }
    }
}
