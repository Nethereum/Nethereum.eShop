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
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.PoStorage;

namespace Nethereum.Commerce.ContractDeployments.Console
{
    class Program
    {
        private const string ESHOP_SYSTEM_ID = "Nethereum.eShop";
        private const string ESHOP_SYSTEM_DESCRIPTION = "Satoshi's Books";
        private const string CONTRACT_NAME_ADDRESS_REGISTRY = "AddressRegistry";
        private const string CONTRACT_NAME_ETERNAL_STORAGE = "EternalStorage";
        private const string CONTRACT_NAME_BUSINESS_PARTNER_STORAGE = "BusinessPartnerStorage";
        private const string CONTRACT_NAME_PO_STORAGE = "PoStorage";
                
        static async Task Main(string[] args)
        {
            try
            {
                System.Console.WriteLine($"Deploying eShop: {ESHOP_SYSTEM_ID} {ESHOP_SYSTEM_DESCRIPTION}...");
                var url = "http://testchain.nethereum.com:8545";
                // Owner is 0x94618601fe6cb8912b274e5a00453949a57f8c1e
                var privateKey = "0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0";
                var account = new Account(privateKey);
                var web3 = new Web3.Web3(account, url);

                //-----------------------------------------------------------------------------------
                // Contract deployments
                //-----------------------------------------------------------------------------------
                // Deploy Address Registry
                System.Console.WriteLine();
                var contractName = CONTRACT_NAME_ADDRESS_REGISTRY;
                System.Console.WriteLine($"Deploying {contractName}...");
                var addressRegDeployment = new AddressRegistryDeployment();
                var addressRegService = await AddressRegistryService.DeployContractAndGetServiceAsync(web3, addressRegDeployment);
                var addressRegOwner = await addressRegService.OwnerQueryAsync();
                System.Console.WriteLine($"{contractName} address is: {addressRegService.ContractHandler.ContractAddress}");
                System.Console.WriteLine($"{contractName} owner is  : {addressRegOwner}");

                // Deploy Eternal Storage
                System.Console.WriteLine();
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                System.Console.WriteLine($"Deploying {contractName}...");
                var eternalStorageDeployment = new EternalStorageDeployment();
                var eternalStorageService = await EternalStorageService.DeployContractAndGetServiceAsync(web3, eternalStorageDeployment);
                var eternalStorageOwner = await eternalStorageService.OwnerQueryAsync();
                System.Console.WriteLine($"{contractName} address is: {eternalStorageService.ContractHandler.ContractAddress}");
                System.Console.WriteLine($"{contractName} owner is  : {eternalStorageOwner}");

                // Deploy Business Partner Storage
                System.Console.WriteLine();
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                System.Console.WriteLine($"Deploying {contractName}...");
                var bpStorageDeployment = new BusinessPartnerStorageDeployment() { ContractAddressOfRegistry = addressRegService.ContractHandler.ContractAddress };
                var bpStorageService = await BusinessPartnerStorageService.DeployContractAndGetServiceAsync(web3, bpStorageDeployment);
                var bpStorageOwner = await bpStorageService.OwnerQueryAsync();
                System.Console.WriteLine($"{contractName} address is: {bpStorageService.ContractHandler.ContractAddress}");
                System.Console.WriteLine($"{contractName} owner is  : {bpStorageOwner}");

                // Deploy PO Storage
                System.Console.WriteLine();
                contractName = CONTRACT_NAME_PO_STORAGE;
                System.Console.WriteLine($"Deploying {contractName}...");
                var poStorageDeployment = new PoStorageDeployment() { ContractAddressOfRegistry = addressRegService.ContractHandler.ContractAddress };
                var poStorageService = await PoStorageService.DeployContractAndGetServiceAsync(web3, poStorageDeployment);
                var poStorageOwner = await poStorageService.OwnerQueryAsync();
                System.Console.WriteLine($"{contractName} address is: {poStorageService.ContractHandler.ContractAddress}");
                System.Console.WriteLine($"{contractName} owner is  : {poStorageOwner}");

                //-----------------------------------------------------------------------------------
                // Configure Address Registry
                //-----------------------------------------------------------------------------------
                System.Console.WriteLine();
                System.Console.WriteLine($"Configuring Address Registry...");

                // Add address entry for eternal storage
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                System.Console.Write($"Configuring Address Registry, adding {contractName}...");
                var txReceipt = await addressRegService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, eternalStorageService.ContractHandler.ContractAddress);                
                System.Console.WriteLine($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for BP storage 
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                System.Console.Write($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await addressRegService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, bpStorageService.ContractHandler.ContractAddress);
                System.Console.WriteLine($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for PO storage
                contractName = CONTRACT_NAME_PO_STORAGE;
                System.Console.Write($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await addressRegService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, poStorageService.ContractHandler.ContractAddress);
                System.Console.WriteLine($"Tx status: {txReceipt.Status.Value}");

                // Authorisations. Nothing needed.

                //-----------------------------------------------------------------------------------
                // Configure Eternal Storage
                //-----------------------------------------------------------------------------------
                // Authorisations. Bind all contracts that will use eternal storage
                System.Console.WriteLine();
                System.Console.WriteLine($"Authorisations for Eternal Storage...");
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                System.Console.Write($"Configuring Eternal Storage, binding {contractName}...");
                txReceipt = await eternalStorageService.BindAddressRequestAndWaitForReceiptAsync(bpStorageService.ContractHandler.ContractAddress);
                System.Console.WriteLine($"Tx status: {txReceipt.Status.Value}");

                contractName = CONTRACT_NAME_PO_STORAGE;
                System.Console.Write($"Configuring Eternal Storage, binding {contractName}...");
                txReceipt = await eternalStorageService.BindAddressRequestAndWaitForReceiptAsync(poStorageService.ContractHandler.ContractAddress);
                System.Console.WriteLine($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Business Partner Storage
                //-----------------------------------------------------------------------------------
                // Tell BP storage the name of the eternal storage contract it needs
                System.Console.WriteLine();
                System.Console.Write($"Configuring BP Storage...");
                txReceipt = await bpStorageService.ConfigureRequestAndWaitForReceiptAsync(CONTRACT_NAME_ETERNAL_STORAGE);
                System.Console.WriteLine($"Tx status: {txReceipt.Status.Value}");

                // Add some BP master data
                System.Console.Write($"Adding eShop master data...");
                var sdf = new SetSystemDescriptionFunction() { SystemId = ESHOP_SYSTEM_ID, SystemDescription = ESHOP_SYSTEM_DESCRIPTION };
                txReceipt = await bpStorageService.SetSystemDescriptionRequestAndWaitForReceiptAsync(sdf);
                System.Console.WriteLine($"Tx status: {txReceipt.Status.Value}");

                // TODO Cant configure this till next layer deployed
                var waf = new SetWalletAddressFunction() { SystemId = ESHOP_SYSTEM_ID, WalletAddress = "" };
                //await bpStorageService.SetWalletAddressRequestAndWaitForReceiptAsync(waf);

                // Authorisations. Bind all contracts that will use BP storage
                // TODO Cant configure this till next layer deployed
                //System.Console.WriteLine($"Authorisations for Eternal Storage...");
                //contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                //System.Console.WriteLine($"Configuring Eternal Storage, binding {contractName}...");
                //txReceipt = await eternalStorageService.BindAddressRequestAndWaitForReceiptAsync(bpStorageService.ContractHandler.ContractAddress);
                //System.Console.WriteLine($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure PO Storage
                //-----------------------------------------------------------------------------------
                // Tell PO storage the name of the eternal storage contract it needs
                System.Console.WriteLine();
                System.Console.Write($"Configuring PO Storage...");
                txReceipt = await poStorageService.ConfigureRequestAndWaitForReceiptAsync(CONTRACT_NAME_ETERNAL_STORAGE);
                System.Console.WriteLine($"Tx status: {txReceipt.Status.Value}");

                // Authorisations. Bind all contracts that will use PO storage
                // TODO Cant configure this till next layer deployed
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
