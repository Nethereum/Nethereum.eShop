using System;
using System.Configuration;
using Microsoft.Azure;
using Microsoft.WindowsAzure;


namespace Nethereum.eShop.WebJobs
{

    public class EshopConfiguration
    {
        public string EthereumRpcUrl { get; set; }
        public string AccountPrivateKey { get; set; }

        public string SellerId { get; set; }

        public string WalletAddress { get; set; }
    }

    public static class ConfigurationSettings
    {
        public const string ETHEREUM_RPC_URL_KEY = "EthereumRPCUrl";
        public const string WORK_REGISTRY_CONTRACT_ADRESS_KEY = "WorkRegistryContractAddress";
        public const string START_PROCESS_FROM_BLOCK_NUMBER_KEY = "StartProcessWorkRegistryFromBlockNumber";

        public static string GetEthereumRPCUrl()
        {
            return CloudConfigurationManager.GetSetting(ETHEREUM_RPC_URL_KEY);
        }

        public static string GetWorkRegistryContractAddress()
        {
            return CloudConfigurationManager.GetSetting(WORK_REGISTRY_CONTRACT_ADRESS_KEY);
        }

        public static ulong StartProcessFromBlockNumber()
        {
            var blockNubmerString = CloudConfigurationManager.GetSetting(START_PROCESS_FROM_BLOCK_NUMBER_KEY);
            if (string.IsNullOrEmpty(blockNubmerString)) return 0;
            return Convert.ToUInt64(blockNubmerString);
        }

        public static bool VerifyConfiguration()
        {
            //string webJobsDashboard = ConfigurationManager.ConnectionStrings["AzureWebJobsDashboard"].ConnectionString;
            //string webJobsStorage = ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString;


            bool configOK = true;
            //if (string.IsNullOrWhiteSpace(webJobsDashboard) || string.IsNullOrWhiteSpace(webJobsStorage))
            //{
            //    configOK = false;
            //    Console.WriteLine("Please add the Azure Storage account credentials in App.config");

            //}

            //if (string.IsNullOrEmpty(GetEthereumRPCUrl()))
            //{
            //    configOK = false;
            //    Console.WriteLine("Please add the ethereum rpc url to the configuration");

            //}

            //if (string.IsNullOrEmpty(GetWorkRegistryContractAddress()))
            //{
            //    configOK = false;
            //    Console.WriteLine("Please add the work registry contract address to the configuration");

            //}
            return configOK;
        }
    }
}