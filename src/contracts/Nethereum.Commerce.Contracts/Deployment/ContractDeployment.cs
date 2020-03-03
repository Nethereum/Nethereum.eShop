using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
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
using Nethereum.Web3;
using System;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Deploy a new set of eShop contracts, or connect to an existing set.
    /// Usage: create ContractDeployment and call InitializeAsync().
    /// </summary>
    public class ContractDeployment
    {
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

        // Configuration
        public readonly ContractDeploymentConfig ContractDeploymentConfig;
        public readonly ContractConnectExistingConfig ContractConnectExistingConfig;

        // Contract names used internally by eg address registry
        public const string CONTRACT_NAME_ADDRESS_REGISTRY = "AddressRegistry";
        public const string CONTRACT_NAME_ETERNAL_STORAGE = "EternalStorage";
        public const string CONTRACT_NAME_BUSINESS_PARTNER_STORAGE = "BusinessPartnerStorage";
        public const string CONTRACT_NAME_PO_STORAGE = "PoStorage";
        public const string CONTRACT_NAME_WALLET_BUYER = "WalletBuyer";
        public const string CONTRACT_NAME_WALLET_SELLER = "WalletSeller";
        public const string CONTRACT_NAME_PURCHASING = "Purchasing";
        public const string CONTRACT_NAME_FUNDING = "Funding";

        private Web3.Web3 _web3;
        private ILogger _logger;
        private readonly bool _isToConnectToExistingDeployment;

        /// <summary>
        /// Deploy a new set of eShop contracts
        /// </summary>        
        public ContractDeployment(IWeb3 web3, ContractDeploymentConfig contractDeploymentConfig, ILogger logger = null)
        {
            Guard.Against.Null(web3, nameof(web3));
            Guard.Against.NullOrWhiteSpace(contractDeploymentConfig.EShopSellerId, nameof(contractDeploymentConfig.EShopSellerId));
            Guard.Against.NullOrWhiteSpace(contractDeploymentConfig.EShopDescription, nameof(contractDeploymentConfig.EShopDescription));
            Guard.Against.NullOrWhiteSpace(contractDeploymentConfig.EShopApproverAddress, nameof(contractDeploymentConfig.EShopApproverAddress));
            _web3 = (Web3.Web3)web3; // code-genned classes require web3, not an iweb3
            ContractDeploymentConfig = contractDeploymentConfig;
            _logger = logger;
            _isToConnectToExistingDeployment = false;
        }

        /// <summary>
        /// Connect to an existing set of eShop contracts
        /// </summary>        
        public ContractDeployment(IWeb3 web3, ContractConnectExistingConfig contractConnectExistingConfig, ILogger logger = null)
        {
            Guard.Against.Null(web3, nameof(web3));
            Guard.Against.NullOrWhiteSpace(contractConnectExistingConfig.WalletBuyerAddress, nameof(contractConnectExistingConfig.WalletBuyerAddress));
            Guard.Against.NullOrWhiteSpace(contractConnectExistingConfig.WalletSellerAddress, nameof(contractConnectExistingConfig.WalletSellerAddress));
            _web3 = (Web3.Web3)web3; // code-genned classes require web3, not an iweb3
            ContractConnectExistingConfig = contractConnectExistingConfig;
            _logger = logger;
            _isToConnectToExistingDeployment = true;
        }

        public async Task InitializeAsync()
        {
            if (_isToConnectToExistingDeployment)
            {
                // Connect to an existing deployment
                await ConnectToAnExistingDeploymentAsync();
            }
            else
            {
                // Make a whole new deployment
                await DeployAndConfigureEShopAsync();
                if (ContractDeploymentConfig.AlsoDeployMockContracts)
                {
                    await DeployMockContractsAsync();
                }
            }
        }

        private async Task DeployAndConfigureEShopAsync()
        {
            try
            {
                Log($"Deploying eShop: {ContractDeploymentConfig.EShopSellerId} {ContractDeploymentConfig.EShopDescription}...");

                //-----------------------------------------------------------------------------------
                // Contract deployments
                //-----------------------------------------------------------------------------------
                #region contract deployments
                // Deploy Address Registry
                Log();
                var contractName = CONTRACT_NAME_ADDRESS_REGISTRY;
                Log($"Deploying {contractName}...");
                var addressRegDeployment = new AddressRegistryDeployment();
                AddressRegistryService = await AddressRegistryService.DeployContractAndGetServiceAsync(_web3, addressRegDeployment);
                var addressRegOwner = await AddressRegistryService.OwnerQueryAsync();
                Log($"{contractName} address is: {AddressRegistryService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {addressRegOwner}");

                // Deploy Eternal Storage
                Log();
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                Log($"Deploying {contractName}...");
                var eternalStorageDeployment = new EternalStorageDeployment();
                EternalStorageService = await EternalStorageService.DeployContractAndGetServiceAsync(_web3, eternalStorageDeployment);
                var eternalStorageOwner = await EternalStorageService.OwnerQueryAsync();
                Log($"{contractName} address is: {EternalStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {eternalStorageOwner}");

                // Deploy Business Partner Storage
                Log();
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                Log($"Deploying {contractName}...");
                var bpStorageDeployment = new BusinessPartnerStorageDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                BusinessPartnerStorageService = await BusinessPartnerStorageService.DeployContractAndGetServiceAsync(_web3, bpStorageDeployment);
                var bpStorageOwner = await BusinessPartnerStorageService.OwnerQueryAsync();
                Log($"{contractName} address is: {BusinessPartnerStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {bpStorageOwner}");

                // Deploy PO Storage
                Log();
                contractName = CONTRACT_NAME_PO_STORAGE;
                Log($"Deploying {contractName}...");
                var poStorageDeployment = new PoStorageDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                PoStorageService = await PoStorageService.DeployContractAndGetServiceAsync(_web3, poStorageDeployment);
                var poStorageOwner = await PoStorageService.OwnerQueryAsync();
                Log($"{contractName} address is: {PoStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {poStorageOwner}");

                // Deploy Wallet Buyer
                Log();
                contractName = CONTRACT_NAME_WALLET_BUYER;
                Log($"Deploying {contractName}...");
                var walletBuyerDeployment = new WalletBuyerDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                WalletBuyerService = await WalletBuyerService.DeployContractAndGetServiceAsync(_web3, walletBuyerDeployment);
                var walletBuyerOwner = await WalletBuyerService.OwnerQueryAsync();
                Log($"{contractName} address is: {WalletBuyerService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {walletBuyerOwner}");

                // Deploy Wallet Seller
                Log();
                contractName = CONTRACT_NAME_WALLET_SELLER;
                Log($"Deploying {contractName}...");
                var walletSellerDeployment = new WalletSellerDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                WalletSellerService = await WalletSellerService.DeployContractAndGetServiceAsync(_web3, walletSellerDeployment);
                var walletSellerOwner = await WalletSellerService.OwnerQueryAsync();
                Log($"{contractName} address is: {WalletSellerService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {walletSellerOwner}");

                // Deploy Purchasing
                Log();
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Deploying {contractName}...");
                var purchasingDeployment = new PurchasingDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                PurchasingService = await PurchasingService.DeployContractAndGetServiceAsync(_web3, purchasingDeployment);
                var purchasingOwner = await PurchasingService.OwnerQueryAsync();
                Log($"{contractName} address is: {PurchasingService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {purchasingOwner}");

                // Deploy Funding
                Log();
                contractName = CONTRACT_NAME_FUNDING;
                Log($"Deploying {contractName}...");
                var fundingDeployment = new FundingDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                FundingService = await FundingService.DeployContractAndGetServiceAsync(_web3, fundingDeployment);
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

                // Authorisations.
                // Bind all contracts that will use BP storage here - currently none other than owner                

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

                // Authorisations. Bind all contracts that will use Purchasing                
                Log($"Authorisations for Purchasing...");
                // Bind WalletBuyer to Purchasing
                contractName = CONTRACT_NAME_WALLET_BUYER;
                Log($"Configuring Purchasing, binding {contractName}...");
                txReceipt = await PurchasingService.BindAddressRequestAndWaitForReceiptAsync(WalletBuyerService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");
                // Bind WalletSeller to Purchasing
                contractName = CONTRACT_NAME_WALLET_SELLER;
                Log($"Configuring Purchasing, binding {contractName}...");
                txReceipt = await PurchasingService.BindAddressRequestAndWaitForReceiptAsync(WalletSellerService.ContractHandler.ContractAddress);
                Log($"Tx status: {txReceipt.Status.Value}");
                // Bind Funding to Purchasing
                contractName = CONTRACT_NAME_FUNDING;
                Log($"Configuring Purchasing, binding {contractName}...");
                txReceipt = await PurchasingService.BindAddressRequestAndWaitForReceiptAsync(FundingService.ContractHandler.ContractAddress);
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

        private async Task DeployMockContractsAsync()
        {
            LogSeparator();
            var contractName = "MockDai";
            Log($"Deploying {contractName}...");

            try
            {
                var mockDaiDeployment = new MockDaiDeployment();
                MockDaiService = await MockDaiService.DeployContractAndGetServiceAsync(_web3, mockDaiDeployment);
                var mockDaiOwner = await MockDaiService.OwnerQueryAsync();
                Log($"{contractName} address is: {MockDaiService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {mockDaiOwner}");
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


        private async Task ConnectToAnExistingDeploymentAsync()
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        private void LogSeparator()
        {
            Log();
            Log("----------------------------------------------------------------");
            Log();
        }

        private void Log() => Log(string.Empty);

        private void Log(string message)
        {
            if (_logger != null)
            {
                _logger.LogInformation(message);
            }
        }
    }
}
