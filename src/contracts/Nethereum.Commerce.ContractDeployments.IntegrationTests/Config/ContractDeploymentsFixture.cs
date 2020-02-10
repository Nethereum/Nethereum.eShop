using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.AddressRegistry;
using Nethereum.Commerce.Contracts.AddressRegistry.ContractDefinition;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.EternalStorage;
using Nethereum.Commerce.Contracts.EternalStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.Funding;
using Nethereum.Commerce.Contracts.Funding.ContractDefinition;
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
        // Deployed contract services
        public AddressRegistryService AddressRegService { get; internal set; }
        public EternalStorageService EternalStorageService { get; internal set; }
        public BusinessPartnerStorageService BusinessPartnerStorageService { get; internal set; }
        public PoStorageService PoStorageService { get; internal set; }
        public WalletBuyerService WalletBuyerService { get; internal set; }
        public WalletSellerService WalletSellerService { get; internal set; }
        public PurchasingService PurchasingService { get; internal set; }
        public FundingService FundingService { get; internal set; }

        // Shared test data
        public Buyer.Po PoTest { get; internal set; }

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

        private void Log(string message = "")
        {
            _diagnosticMessageSink.OnMessage(new DiagnosticMessage(message));
        }

        public async Task InitializeAsync()
        {
            await DeployAndConfigureEShop();
            await CreateSharedTransactionData();
        }

        private async Task DeployAndConfigureEShop()
        {
            try
            {
                Log($"Deploying eShop: {ContractDeploymentConfig.EShopSellerId} {ContractDeploymentConfig.EShopDescription}...");
                var url = ContractDeploymentConfig.BlockchainUrl;
                var privateKey = ContractDeploymentConfig.ContractDeploymentOwnerPrivateKey;
                var account = new Account(privateKey);
                var web3 = new Web3.Web3(account, url);

                //-----------------------------------------------------------------------------------
                // Contract deployments
                //-----------------------------------------------------------------------------------
                #region contract deployments
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

                // Deploy Wallet Buyer
                Log();
                contractName = CONTRACT_NAME_WALLET_BUYER;
                Log($"Deploying {contractName}...");
                var walletBuyerDeployment = new WalletBuyerDeployment() { ContractAddressOfRegistry = AddressRegService.ContractHandler.ContractAddress };
                WalletBuyerService = await WalletBuyerService.DeployContractAndGetServiceAsync(web3, walletBuyerDeployment);
                var walletBuyerOwner = await WalletBuyerService.OwnerQueryAsync();
                Log($"{contractName} address is: {WalletBuyerService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {walletBuyerOwner}");

                // Deploy Wallet Seller
                Log();
                contractName = CONTRACT_NAME_WALLET_SELLER;
                Log($"Deploying {contractName}...");
                var walletSellerDeployment = new WalletSellerDeployment() { ContractAddressOfRegistry = AddressRegService.ContractHandler.ContractAddress };
                WalletSellerService = await WalletSellerService.DeployContractAndGetServiceAsync(web3, walletSellerDeployment);
                var walletSellerOwner = await WalletSellerService.OwnerQueryAsync();
                Log($"{contractName} address is: {WalletSellerService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {walletSellerOwner}");

                // Deploy Purchasing
                Log();
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Deploying {contractName}...");
                var purchasingDeployment = new PurchasingDeployment() { ContractAddressOfRegistry = AddressRegService.ContractHandler.ContractAddress };
                PurchasingService = await PurchasingService.DeployContractAndGetServiceAsync(web3, purchasingDeployment);
                var purchasingOwner = await PurchasingService.OwnerQueryAsync();
                Log($"{contractName} address is: {PurchasingService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {purchasingOwner}");

                // Deploy Funding
                Log();
                contractName = CONTRACT_NAME_FUNDING;
                Log($"Deploying {contractName}...");
                var fundingDeployment = new FundingDeployment() { ContractAddressOfRegistry = AddressRegService.ContractHandler.ContractAddress };
                FundingService = await FundingService.DeployContractAndGetServiceAsync(web3, fundingDeployment);
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

                // Add address entry for Purchasing
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, PurchasingService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for Funding
                contractName = CONTRACT_NAME_FUNDING;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegService.RegisterAddressStringRequestAndWaitForReceiptAsync(contractName, FundingService.ContractHandler.ContractAddress);
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
                // TODO Cant configure this till next layer deployed

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

        private async Task CreateSharedTransactionData()
        {
            Log();
            Log("----------------------------------------------------------------");
            Log();
            Log($"Creating shared transaction data...");

            // Create a PO directly in the po store that can be used as shared data for many tests
            uint poNumber = GetRandomInt();
            string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            var po = CreateTestPo(poNumber, approverAddress, quoteId);

            // Store PO
            var txReceipt = await PoStorageService.SetPoRequestAndWaitForReceiptAsync(po);
            Log($"Tx status: {txReceipt.Status.Value}");

            PoTest = po.ToBuyerPo();

        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
