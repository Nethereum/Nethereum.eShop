using Nethereum;
using Nethereum.Web3.Accounts;
using Nethereum.Web3;
using System;
using System.Threading.Tasks;
using Nethereum.Commerce.Contracts.AddressRegistry;
using Nethereum.Commerce.Contracts.AddressRegistry.ContractDefinition;
using Nethereum.Commerce.Contracts.EternalStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.EternalStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;

namespace Nethereum.Commerce.ContractDeployments.Console
{
    class Program
    {
        private const string CONTRACT_NAME_ADDRESS_REGISTRY = "AddressRegistry";
        private const string CONTRACT_NAME_ETERNAL_STORAGE = "EternalStorage";
        private const string CONTRACT_NAME_BUSINESS_PARTNER_STORAGE = "BusinessPartnerStorage";
        private const string CONTRACT_NAME_PO_STORAGE = "PoStorage";
                
        static async Task Main(string[] args)
        {
            try
            {
                var url = "http://testchain.nethereum.com:8545";
                // Owner is 0x94618601fe6cb8912b274e5a00453949a57f8c1e
                var privateKey = "0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0";
                var account = new Account(privateKey);
                var web3 = new Web3.Web3(account, url);

                //-----------------------------------------------------------------------------------
                // Contract deployments
                //-----------------------------------------------------------------------------------
                // Deploy Address Registry
                var contractName = CONTRACT_NAME_ADDRESS_REGISTRY;
                System.Console.WriteLine($"Deploying {contractName}...");
                var addressRegDeployment = new AddressRegistryDeployment();
                var addressRegSvc = await AddressRegistryService.DeployContractAndGetServiceAsync(web3, addressRegDeployment);
                var addressRegOwner = await addressRegSvc.OwnerQueryAsync();
                System.Console.WriteLine($"{contractName} address is: {addressRegSvc.ContractHandler.ContractAddress}");
                System.Console.WriteLine($"{contractName} owner is  : {addressRegOwner}");
                
                // Deploy Eternal Storage
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                System.Console.WriteLine($"Deploying {contractName}...");
                var eternalStorageDeployment = new EternalStorageDeployment();
                var eternalStorageSvc = await EternalStorageService.DeployContractAndGetServiceAsync(web3, eternalStorageDeployment);
                var eternalStorageOwner = await eternalStorageSvc.OwnerQueryAsync();
                System.Console.WriteLine($"{contractName} address is: {eternalStorageSvc.ContractHandler.ContractAddress}");
                System.Console.WriteLine($"{contractName} owner is  : {eternalStorageOwner}");

                // Deploy Business Partner Storage
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                System.Console.WriteLine($"Deploying {contractName}...");
                var bpStorageDeployment = new BusinessPartnerStorageDeployment() { ContractAddressOfRegistry = addressRegSvc.ContractHandler.ContractAddress };
                var bpStorageSvc = await BusinessPartnerStorageService.DeployContractAndGetServiceAsync(web3, bpStorageDeployment);
                var bpStorageOwner = await bpStorageSvc.OwnerQueryAsync();
                System.Console.WriteLine($"{contractName} address is: {bpStorageSvc.ContractHandler.ContractAddress}");
                System.Console.WriteLine($"{contractName} owner is  : {bpStorageOwner}");
                
                // Deploy PO Storage



                //-----------------------------------------------------------------------------------
                // Configure Address Registry
                //-----------------------------------------------------------------------------------
                // Add entry for eternal storage
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                System.Console.WriteLine($"Configuring Address Registry, adding {contractName}...");
                var r = await addressRegSvc.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, eternalStorageSvc.ContractHandler.ContractAddress);                
                System.Console.WriteLine($"Config tx status: {r.Status.Value}");

                // Add entry for BP storage 
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                System.Console.WriteLine($"Configuring Address Registry, adding {contractName}...");
                r = await addressRegSvc.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, bpStorageSvc.ContractHandler.ContractAddress);
                System.Console.WriteLine($"Config tx status: {r.Status.Value}");

                // Add entry for PO storage


                //-----------------------------------------------------------------------------------
                // Configure BP Storage
                //-----------------------------------------------------------------------------------
                // call .configure
                // and add some master data

                //-----------------------------------------------------------------------------------
                // Configure PO Storage
                //-----------------------------------------------------------------------------------
                // call .configure

                // then separate test project

            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Exception thrown: {ex.Message}");
            }
            finally
            {
                System.Console.WriteLine($"Press Enter to close.");
                System.Console.ReadLine();
            }
        }
    }
}
