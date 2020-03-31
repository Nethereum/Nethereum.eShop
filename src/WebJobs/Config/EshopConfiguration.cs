using System;
using System.Configuration;
using System.Numerics;
using Microsoft.Azure;
using Microsoft.WindowsAzure;


namespace Nethereum.eShop.WebJobs.Configuration
{

    public class EshopConfiguration
    {
        public string EthereumRpcUrl { get; set; }
        public string AccountPrivateKey { get; set; }

        public string EShopId { get; set; }

        public string BuyerWalletAddress { get; set; }

        public string BusinessPartnerStorageServiceAddress { get; set; }

        public bool CreateFakePurchaseOrders { get; set; }

        public PurchaseOrderEventLogProcessingConfiguration PurchaseOrderEventLogConfiguration { get; set; }
    }

    public class PurchaseOrderEventLogProcessingConfiguration
    {
        public bool Enabled { get; set; } = true;

        public string BlockProgressJsonFile { get; set; }

        public string MinimumStartingBlock { get; set; } = "0";

        public uint NumberOfBlocksPerBatch { get; set; } = 10;

        public uint MinimumBlockConfirmations { get; set; } = 12;

        public BigInteger GetMinimumStartingBlock() => BigInteger.Parse(MinimumStartingBlock);

        public int TimeoutMs { get; set; }
    }

}