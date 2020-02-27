using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.AddressRegistry;
using Nethereum.Commerce.Contracts.AddressRegistry.ContractDefinition;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.EternalStorage;
using Nethereum.Commerce.Contracts.EternalStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.Funding;
using Nethereum.Commerce.Contracts.Funding.ContractDefinition;
using Nethereum.Commerce.Contracts.MockDai;
using Nethereum.Commerce.Contracts.MockDai.ContractDefinition;
using Nethereum.Commerce.Contracts.PoStorage;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.Purchasing;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Commerce.Contracts.WalletBuyer;
using Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;
using Nethereum.Commerce.Contracts.WalletSeller;
using Nethereum.Commerce.Contracts.WalletSeller.ContractDefinition;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoHelpers;
using Buyer = Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class ContractDeploymentsFixture : IAsyncLifetime
    {
        public Web3.Web3 Web3 { get; internal set; }

        // Deployed contract services
        public AddressRegistryService AddressRegistryService { get; internal set; }
        public EternalStorageService EternalStorageService { get; internal set; }
        public BusinessPartnerStorageService BusinessPartnerStorageService { get; internal set; }
        public PoStorageService PoStorageService { get; internal set; }
        public WalletBuyerService WalletBuyerService { get; internal set; }
        public WalletSellerService WalletSellerService { get; internal set; }
        public PurchasingService PurchasingService { get; internal set; }
        public FundingService FundingService { get; internal set; }

        // Mocks
        public MockDaiService MockDaiService { get; internal set; }
        public string MockDaiSymbol { get; internal set; }

        // Configuration
        public readonly ContractDeploymentsConfig ContractDeploymentConfig;
        // Contract names used internally by eg address registry
        public const string CONTRACT_NAME_ADDRESS_REGISTRY = "AddressRegistry";
        public const string CONTRACT_NAME_ETERNAL_STORAGE = "EternalStorage";
        public const string CONTRACT_NAME_BUSINESS_PARTNER_STORAGE = "BusinessPartnerStorage";
        public const string CONTRACT_NAME_PO_STORAGE = "PoStorage";
        public const string CONTRACT_NAME_WALLET_BUYER = "WalletBuyer";
        public const string CONTRACT_NAME_WALLET_SELLER = "WalletSeller";
        public const string CONTRACT_NAME_PURCHASING = "Purchasing";
        public const string CONTRACT_NAME_FUNDING = "Funding";

        private readonly IMessageSink _diagnosticMessageSink;

        public ContractDeploymentsFixture(IMessageSink diagnosticMessageSink)
        {
            _diagnosticMessageSink = diagnosticMessageSink;
            var appConfig = ConfigurationUtils.Build(Array.Empty<string>(), "UserSecret");
            ContractDeploymentConfig = ContractDeploymentsConfigFactory.Get(appConfig);
        }

        public async Task InitializeAsync()
        {
            await DeployAndConfigureEShop();
            await DeployMockContracts();
        }

        private async Task DeployAndConfigureEShop()
        {
            try
            {
                Log($"Deploying eShop: {ContractDeploymentConfig.EShopSellerId} {ContractDeploymentConfig.EShopDescription}...");
                var url = ContractDeploymentConfig.BlockchainUrl;
                var privateKey = ContractDeploymentConfig.ContractDeploymentOwnerPrivateKey;
                var account = new Account(privateKey);
                Web3 = new Web3.Web3(account, url);

                //-----------------------------------------------------------------------------------
                // Contract deployments
                //-----------------------------------------------------------------------------------
                #region contract deployments
                // Deploy Address Registry
                Log();
                var contractName = CONTRACT_NAME_ADDRESS_REGISTRY;
                Log($"Deploying {contractName}...");
                var addressRegDeployment = new AddressRegistryDeployment();
                AddressRegistryService = await AddressRegistryService.DeployContractAndGetServiceAsync(Web3, addressRegDeployment);
                var addressRegOwner = await AddressRegistryService.OwnerQueryAsync();
                Log($"{contractName} address is: {AddressRegistryService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {addressRegOwner}");

                // Deploy Eternal Storage
                Log();
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                Log($"Deploying {contractName}...");
                var eternalStorageDeployment = new EternalStorageDeployment();
                EternalStorageService = await EternalStorageService.DeployContractAndGetServiceAsync(Web3, eternalStorageDeployment);
                var eternalStorageOwner = await EternalStorageService.OwnerQueryAsync();
                Log($"{contractName} address is: {EternalStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {eternalStorageOwner}");

                // Deploy Business Partner Storage
                Log();
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                Log($"Deploying {contractName}...");
                var bpStorageDeployment = new BusinessPartnerStorageDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                BusinessPartnerStorageService = await BusinessPartnerStorageService.DeployContractAndGetServiceAsync(Web3, bpStorageDeployment);
                var bpStorageOwner = await BusinessPartnerStorageService.OwnerQueryAsync();
                Log($"{contractName} address is: {BusinessPartnerStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {bpStorageOwner}");

                // Deploy PO Storage
                Log();
                contractName = CONTRACT_NAME_PO_STORAGE;
                Log($"Deploying {contractName}...");
                var poStorageDeployment = new PoStorageDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                PoStorageService = await PoStorageService.DeployContractAndGetServiceAsync(Web3, poStorageDeployment);
                var poStorageOwner = await PoStorageService.OwnerQueryAsync();
                Log($"{contractName} address is: {PoStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {poStorageOwner}");

                // Deploy Wallet Buyer
                Log();
                contractName = CONTRACT_NAME_WALLET_BUYER;
                Log($"Deploying {contractName}...");
                var walletBuyerDeployment = new WalletBuyerDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                WalletBuyerService = await WalletBuyerService.DeployContractAndGetServiceAsync(Web3, walletBuyerDeployment);
                var walletBuyerOwner = await WalletBuyerService.OwnerQueryAsync();
                Log($"{contractName} address is: {WalletBuyerService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {walletBuyerOwner}");

                // Deploy Wallet Seller
                Log();
                contractName = CONTRACT_NAME_WALLET_SELLER;
                Log($"Deploying {contractName}...");
                var walletSellerDeployment = new WalletSellerDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                WalletSellerService = await WalletSellerService.DeployContractAndGetServiceAsync(Web3, walletSellerDeployment);
                var walletSellerOwner = await WalletSellerService.OwnerQueryAsync();
                Log($"{contractName} address is: {WalletSellerService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {walletSellerOwner}");

                // Deploy Purchasing
                Log();
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Deploying {contractName}...");
                var purchasingDeployment = new PurchasingDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                PurchasingService = await PurchasingService.DeployContractAndGetServiceAsync(Web3, purchasingDeployment);
                var purchasingOwner = await PurchasingService.OwnerQueryAsync();
                Log($"{contractName} address is: {PurchasingService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {purchasingOwner}");

                // Deploy Funding
                Log();
                contractName = CONTRACT_NAME_FUNDING;
                Log($"Deploying {contractName}...");
                var fundingDeployment = new FundingDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                FundingService = await FundingService.DeployContractAndGetServiceAsync(Web3, fundingDeployment);
                var fundingOwner = await FundingService.OwnerQueryAsync();
                Log($"{contractName} address is: {FundingService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {fundingOwner}");

                #endregion

                //-----------------------------------------------------------------------------------
                // Configure Address Registry
                //-----------------------------------------------------------------------------------
                #region configure address registry
                Log();
                Log($"Configuring Address Registry...");

                // Add address entry for eternal storage
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                Log($"Configuring Address Registry, adding {contractName}...");
                var txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, EternalStorageService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for BP storage 
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, BusinessPartnerStorageService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for PO storage
                contractName = CONTRACT_NAME_PO_STORAGE;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, PoStorageService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for Purchasing
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, PurchasingService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for Funding
                contractName = CONTRACT_NAME_FUNDING;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, FundingService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Authorisations. Nothing needed.
                #endregion

                //-----------------------------------------------------------------------------------
                // Configure Eternal Storage
                //-----------------------------------------------------------------------------------
                #region configure eternal storage
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
                #endregion

                //-----------------------------------------------------------------------------------
                // Configure Business Partner Storage
                //-----------------------------------------------------------------------------------
                #region configure business partner storage
                Log();
                Log($"Configuring BP Storage...");
                txReceipt = await BusinessPartnerStorageService.ConfigureRequestAndWaitForReceiptAsync(CONTRACT_NAME_ETERNAL_STORAGE);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add some BP master data
                Log($"Adding eShop master data...");
                txReceipt = await BusinessPartnerStorageService.SetSellerRequestAndWaitForReceiptAsync(
                    new Seller()
                    {
                        SellerId = ContractDeploymentConfig.EShopSellerId,
                        SellerDescription = ContractDeploymentConfig.EShopDescription,
                        ContractAddress = WalletSellerService.ContractHandler.ContractAddress,
                        ApproverAddress = ContractDeploymentConfig.EShopApproverAddress,
                        IsActive = true
                    });
                Log($"Tx status: {txReceipt.Status.Value}");
                #endregion

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
                Log();
                Log($"Configuring PO Storage...");
                txReceipt = await PoStorageService.ConfigureRequestAndWaitForReceiptAsync(CONTRACT_NAME_ETERNAL_STORAGE);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Authorisations. Bind all contracts that will use PO storage
                // Bind Purchasing to PO Storage
                Log($"Authorisations for PO Storage...");
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Configuring PO Storage, binding {contractName}...");
                txReceipt = await PoStorageService.BindAddressRequestAndWaitForReceiptAsync(PurchasingService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Wallet Seller
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring Wallet Seller...");
                txReceipt = await WalletSellerService.ConfigureRequestAndWaitForReceiptAsync(
                    ContractDeploymentConfig.EShopSellerId, CONTRACT_NAME_PURCHASING, CONTRACT_NAME_FUNDING);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Wallet Buyer
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring Wallet Buyer...");
                txReceipt = await WalletBuyerService.ConfigureRequestAndWaitForReceiptAsync(
                    CONTRACT_NAME_PURCHASING, CONTRACT_NAME_FUNDING);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Purchasing
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring Purchasing...");
                txReceipt = await PurchasingService.ConfigureRequestAndWaitForReceiptAsync(
                    CONTRACT_NAME_PO_STORAGE, CONTRACT_NAME_BUSINESS_PARTNER_STORAGE, CONTRACT_NAME_FUNDING);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Funding
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring Funding...");
                txReceipt = await FundingService.ConfigureRequestAndWaitForReceiptAsync(
                    CONTRACT_NAME_PO_STORAGE, CONTRACT_NAME_BUSINESS_PARTNER_STORAGE);
                Log($"Tx status: {txReceipt.Status.Value}");
            }
            catch (Exception ex)
            {
                Log($"Exception thrown: {ex.Message}");
            }
            finally
            {
                Log($"Deploy and configure complete.");
            }
        }

        private async Task DeployMockContracts()
        {
            LogSeparator();
            var contractName = "MockDai";
            Log($"Deploying {contractName}...");

            try
            {
                var mockDaiDeployment = new MockDaiDeployment();
                MockDaiService = await MockDaiService.DeployContractAndGetServiceAsync(Web3, mockDaiDeployment);
                var mockDaiOwner = await MockDaiService.OwnerQueryAsync();
                Log($"{contractName} address is: {MockDaiService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {mockDaiOwner}");
                MockDaiSymbol = await MockDaiService.SymbolQueryAsync();
            }
            catch (Exception ex)
            {
                Log($"Exception thrown: {ex.Message}");
            }
            finally
            {
                Log($"Mock contract deploy complete.");
            }
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        private void Log(string message = "")
        {
            _diagnosticMessageSink.OnMessage(new DiagnosticMessage(message));
        }

        private void LogSeparator()
        {
            Log();
            Log("----------------------------------------------------------------");
            Log();
        }

    }
}
