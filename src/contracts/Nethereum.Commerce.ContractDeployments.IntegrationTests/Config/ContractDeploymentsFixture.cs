using Nethereum.Commerce.Contracts.AddressRegistry;
using Nethereum.Commerce.Contracts.AddressRegistry.ContractDefinition;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.EternalStorage;
using Nethereum.Commerce.Contracts.EternalStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.PoStorage;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class ContractDeploymentsFixture : IAsyncLifetime
    {
        // Deployed contract services
        public AddressRegistryService AddressRegService { get; internal set; }
        public EternalStorageService EternalStorageService { get; internal set; }
        public BusinessPartnerStorageService BusinessPartnerStorageService { get; internal set; }
        public PoStorageService PoStorageService { get; internal set; }

        // Configuration
        public readonly ContractDeploymentsConfig ContractDeploymentConfig;
        // Contract names used internally by eg address registry
        public const string CONTRACT_NAME_ADDRESS_REGISTRY = "AddressRegistry";
        public const string CONTRACT_NAME_ETERNAL_STORAGE = "EternalStorage";
        public const string CONTRACT_NAME_BUSINESS_PARTNER_STORAGE = "BusinessPartnerStorage";
        public const string CONTRACT_NAME_PO_STORAGE = "PoStorage";
 
        private readonly IMessageSink _diagnosticMessageSink;

        public ContractDeploymentsFixture(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
            var appConfig = ConfigurationUtils.Build(Array.Empty<string>(), "UserSecret");
            ContractDeploymentConfig = ContractDeploymentsConfigFactory.Get(appConfig);
        }

        private void Log(string message = "")
        {
            _diagnosticMessageSink.OnMessage(new DiagnosticMessage(message));
        }

        public async Task InitializeAsync()
        {
            try
            {
                Log($"Deploying eShop: {ContractDeploymentConfig.EShopSystemId} {ContractDeploymentConfig.EShopDescription}...");
                var url = ContractDeploymentConfig.BlockchainUrl;
                var privateKey = ContractDeploymentConfig.ContractDeploymentOwnerPrivateKey;
                var account = new Account(privateKey);
                var web3 = new Web3.Web3(account, url);

                //-----------------------------------------------------------------------------------
                // Contract deployments
                //-----------------------------------------------------------------------------------
                // Deploy Address Registry
                Log();
                var contractName = CONTRACT_NAME_ADDRESS_REGISTRY;
                Log($"Deploying {contractName}...");
                var addressRegDeployment = new AddressRegistryDeployment();
                AddressRegService = await AddressRegistryService.DeployContractAndGetServiceAsync(web3, addressRegDeployment);
                var addressRegOwner = await AddressRegService.OwnerQueryAsync();
                Log($"{contractName} address is: {AddressRegService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {addressRegOwner}");

                // Deploy Eternal Storage
                Log();
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                Log($"Deploying {contractName}...");
                var eternalStorageDeployment = new EternalStorageDeployment();
                EternalStorageService = await EternalStorageService.DeployContractAndGetServiceAsync(web3, eternalStorageDeployment);
                var eternalStorageOwner = await EternalStorageService.OwnerQueryAsync();
                Log($"{contractName} address is: {EternalStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {eternalStorageOwner}");

                // Deploy Business Partner Storage
                Log();
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                Log($"Deploying {contractName}...");
                var bpStorageDeployment = new BusinessPartnerStorageDeployment() { ContractAddressOfRegistry = AddressRegService.ContractHandler.ContractAddress };
                BusinessPartnerStorageService = await BusinessPartnerStorageService.DeployContractAndGetServiceAsync(web3, bpStorageDeployment);
                var bpStorageOwner = await BusinessPartnerStorageService.OwnerQueryAsync();
                Log($"{contractName} address is: {BusinessPartnerStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {bpStorageOwner}");

                // Deploy PO Storage
                Log();
                contractName = CONTRACT_NAME_PO_STORAGE;
                Log($"Deploying {contractName}...");
                var poStorageDeployment = new PoStorageDeployment() { ContractAddressOfRegistry = AddressRegService.ContractHandler.ContractAddress };
                PoStorageService = await PoStorageService.DeployContractAndGetServiceAsync(web3, poStorageDeployment);
                var poStorageOwner = await PoStorageService.OwnerQueryAsync();
                Log($"{contractName} address is: {PoStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {poStorageOwner}");

                //-----------------------------------------------------------------------------------
                // Configure Address Registry
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring Address Registry...");

                // Add address entry for eternal storage
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                Log($"Configuring Address Registry, adding {contractName}...");
                var txReceipt = await AddressRegService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, EternalStorageService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for BP storage 
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, BusinessPartnerStorageService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for PO storage
                contractName = CONTRACT_NAME_PO_STORAGE;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, PoStorageService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Authorisations. Nothing needed.

                //-----------------------------------------------------------------------------------
                // Configure Eternal Storage
                //-----------------------------------------------------------------------------------
                // Authorisations. Bind all contracts that will use eternal storage
                Log();
                Log($"Authorisations for Eternal Storage...");
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                Log($"Configuring Eternal Storage, binding {contractName}...");
                txReceipt = await EternalStorageService.BindAddressRequestAndWaitForReceiptAsync(BusinessPartnerStorageService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                contractName = CONTRACT_NAME_PO_STORAGE;
                Log($"Configuring Eternal Storage, binding {contractName}...");
                txReceipt = await EternalStorageService.BindAddressRequestAndWaitForReceiptAsync(PoStorageService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Business Partner Storage
                //-----------------------------------------------------------------------------------
                // Tell BP storage the name of the eternal storage contract it needs
                Log();
                Log($"Configuring BP Storage...");
                txReceipt = await BusinessPartnerStorageService.ConfigureRequestAndWaitForReceiptAsync(CONTRACT_NAME_ETERNAL_STORAGE);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add some BP master data
                Log($"Adding eShop master data...");
                txReceipt = await BusinessPartnerStorageService.SetSystemDescriptionRequestStringAndWaitForReceiptAsync(
                    ContractDeploymentConfig.EShopSystemId, 
                    ContractDeploymentConfig.EShopDescription);
                Log($"Tx status: {txReceipt.Status.Value}");

                // TODO Cant configure this till next layer deployed
                //var waf = new SetWalletAddressFunction() { SystemId = ESHOP_SYSTEM_ID, WalletAddress = "" };
                //await bpStorageService.SetWalletAddressRequestAndWaitForReceiptAsync(waf);

                // Authorisations. Bind all contracts that will use BP storage
                // TODO Cant configure this till next layer deployed
                //Log($"Authorisations for Eternal Storage...");
                //contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                //Log($"Configuring Eternal Storage, binding {contractName}...");
                //txReceipt = await eternalStorageService.BindAddressRequestAndWaitForReceiptAsync(bpStorageService.ContractHandler.ContractAddress);
                //Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure PO Storage
                //-----------------------------------------------------------------------------------
                // Tell PO storage the name of the eternal storage contract it needs
                Log();
                Log($"Configuring PO Storage...");
                txReceipt = await PoStorageService.ConfigureRequestAndWaitForReceiptAsync(CONTRACT_NAME_ETERNAL_STORAGE);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Authorisations. Bind all contracts that will use PO storage
                // TODO Cant configure this till next layer deployed
            }
            catch (Exception ex)
            {
                Log($"Exception thrown: {ex.Message}");
            }
            finally
            {
                Log($"Complete.");
            }
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
