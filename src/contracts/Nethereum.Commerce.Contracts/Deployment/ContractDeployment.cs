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
using Nethereum.Commerce.Contracts.BuyerWallet;
using Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition;
using Nethereum.Commerce.Contracts.SellerAdmin;
using Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;
using Nethereum.Web3;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Deploy a new set of eShop contracts, or connect to an existing set.
    /// Usage: create ContractDeployment and call InitializeAsync().
    /// </summary>
    public class ContractDeployment
    {
        // Deployed contract services
        // Global
        public EternalStorageService EternalStorageServiceGlobal { get; internal set; }
        public BusinessPartnerStorageService BusinessPartnerStorageServiceGlobal { get; internal set; }

        // Local
        public AddressRegistryService AddressRegistryServiceLocal { get; internal set; }
        public EternalStorageService EternalStorageServiceLocal { get; internal set; }
        public PoStorageService PoStorageServiceLocal { get; internal set; }
        public PurchasingService PurchasingServiceLocal { get; internal set; }
        public FundingService FundingServiceLocal { get; internal set; }

        public BuyerWalletService BuyerWalletService { get; internal set; }
        public BuyerWalletService BuyerWalletService02 { get; internal set; }
        public SellerAdminService SellerAdminService { get; internal set; }

        // Mocks
        public MockDaiService MockDaiService { get; internal set; }

        // Configuration
        public readonly ContractNewDeploymentConfig ContractNewDeploymentConfig;
       
        // Contract names used in logs, plus local names used internally by eg address registry
        public const string CONTRACT_NAME_ETERNAL_STORAGE_GLOBAL = "EternalStorageGlobal";
        public const string CONTRACT_NAME_BUSINESS_PARTNER_STORAGE_GLOBAL = "BusinessPartnerStorageGlobal";
        public const string CONTRACT_NAME_ADDRESS_REGISTRY_LOCAL = "AddressRegistryLocal";
        public const string CONTRACT_NAME_ETERNAL_STORAGE_LOCAL = "EternalStorageLocal";
        public const string CONTRACT_NAME_PO_STORAGE_LOCAL = "PoStorageLocal";
        public const string CONTRACT_NAME_PURCHASING_LOCAL = "PurchasingLocal";
        public const string CONTRACT_NAME_FUNDING_LOCAL = "FundingLocal";

        public const string CONTRACT_NAME_BUYER_WALLET = "BuyerWallet";
        public const string CONTRACT_NAME_SELLER_ADMIN = "SellerAdmin";

        private Web3.Web3 _web3;
        private ILogger _logger;

        /// <summary>
        /// Deploy a new set of eShop contracts
        /// </summary>        
        public ContractDeployment(IWeb3 web3, ContractNewDeploymentConfig cdc, ILogger logger = null)
        {
            Guard.Against.Null(web3, nameof(web3));
            _web3 = (Web3.Web3)web3; // code-genned classes require web3, not an iweb3

            // Validate eShop config
            Guard.Against.Null(cdc.Eshop, nameof(cdc.Eshop));
            Guard.Against.NullOrWhiteSpace(cdc.Eshop.EShopId, nameof(cdc.Eshop.EShopId));
            cdc.Eshop.EShopDescription = cdc.Eshop.EShopDescription ?? string.Empty;
            Guard.Against.Zero(cdc.Eshop.QuoteSigners.Count, nameof(cdc.Eshop.QuoteSigners.Count));

            // Validate Seller config
            Guard.Against.Null(cdc.Seller, nameof(cdc.Seller));
            Guard.Against.NullOrWhiteSpace(cdc.Seller.SellerId, nameof(cdc.Seller.SellerId));
            cdc.Seller.SellerDescription = cdc.Seller.SellerDescription ?? string.Empty;

            ContractNewDeploymentConfig = cdc;
            _logger = logger;
        }

        /// <summary>
        /// Deployment
        /// </summary>
        public async Task InitializeAsync()
        {
            // Measure duration, tx count, ETH cost
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var txCountStart = await _web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(
                _web3.TransactionManager.Account.Address).ConfigureAwait(false);
            var ethBalanceStartInWei = await _web3.Eth.GetBalance.SendRequestAsync(
                _web3.TransactionManager.Account.Address).ConfigureAwait(false);

            // New deployment
            await DeployAndConfigureGlobalStorageAsync().ConfigureAwait(false);
            await DeployEShopAsync().ConfigureAwait(false);
            await AddGlobalStorageMasterDataAsync().ConfigureAwait(false);
            await ConfigureEShopAsync().ConfigureAwait(false);

            // With mocks if needed
            if (ContractNewDeploymentConfig.AlsoDeployMockContracts)
            {
                await DeployMockContractsAsync().ConfigureAwait(false);
            }

            // End meaurements
            LogHeader("Metrics");
            var txCountEnd = await _web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(
                _web3.TransactionManager.Account.Address).ConfigureAwait(false);
            var txCountOverall = txCountEnd.Value - txCountStart.Value;
            Log($"Transaction count for deployment: {txCountOverall}");

            var ethBalanceEndInWei = await _web3.Eth.GetBalance.SendRequestAsync(
                _web3.TransactionManager.Account.Address).ConfigureAwait(false);
            var ethCostOverallInWei = ethBalanceStartInWei.Value - ethBalanceEndInWei.Value;
            var ethCostOverall = Web3.Web3.Convert.FromWei(ethCostOverallInWei);
            Log($"Cost for deployment: {ethCostOverall} ETH");
            stopwatch.Stop();
            Log($"Duration for deployment: {stopwatch.ElapsedMilliseconds.ToString("N0")} ms");
            LogHeader("Complete");
        }

        private async Task AddGlobalStorageMasterDataAsync()
        {
            //-----------------------------------------------------------------------------------                                
            // Add some Business Partner master data
            //-----------------------------------------------------------------------------------
            // Need at least one eShop and one Seller to be a useful deployment
            LogHeader("Add Global Master Data (eShops, Sellers)");
            Log($"Add eShop master data...");
            var txReceipt = await BusinessPartnerStorageServiceGlobal.SetEshopRequestAndWaitForReceiptAsync(
                new Eshop()
                {
                    EShopId = ContractNewDeploymentConfig.Eshop.EShopId,
                    EShopDescription = ContractNewDeploymentConfig.Eshop.EShopDescription,
                    PurchasingContractAddress = PurchasingServiceLocal.ContractHandler.ContractAddress,
                    IsActive = true,
                    CreatedByAddress = string.Empty,  // filled by contract
                    QuoteSignerCount = Convert.ToUInt32(ContractNewDeploymentConfig.Eshop.QuoteSigners.Count),
                    QuoteSigners = ContractNewDeploymentConfig.Eshop.QuoteSigners
                }).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            Log($"Adding Seller master data...");
            txReceipt = await BusinessPartnerStorageServiceGlobal.SetSellerRequestAndWaitForReceiptAsync(
                new Seller()
                {
                    SellerId = ContractNewDeploymentConfig.Seller.SellerId,
                    SellerDescription = ContractNewDeploymentConfig.Seller.SellerDescription,
                    AdminContractAddress = SellerAdminService.ContractHandler.ContractAddress,
                    IsActive = true,
                    CreatedByAddress = string.Empty  // filled by contract
                }).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");
        }

        private async Task DeployAndConfigureGlobalStorageAsync()
        {
            //-----------------------------------------------------------------------------------
            // Contract deployments global
            //-----------------------------------------------------------------------------------
            // Deploy Global Eternal Storage
            Log();
            var contractName = CONTRACT_NAME_ETERNAL_STORAGE_GLOBAL;
            Log($"Deploying {contractName}...");
            var eternalStorageDeploymentGlobal = new EternalStorageDeployment();
            EternalStorageServiceGlobal = await EternalStorageService.DeployContractAndGetServiceAsync(
                _web3, eternalStorageDeploymentGlobal).ConfigureAwait(false);
            var eternalStorageOwner = await EternalStorageServiceGlobal.OwnerQueryAsync().ConfigureAwait(false);
            Log($"{contractName} address is: {EternalStorageServiceGlobal.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {eternalStorageOwner}");

            //// Deploy Global Business Partner Storage
            //Log();
            //contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE_GLOBAL;
            //Log($"Deploying {contractName}...");
            //var bpStorageDeploymentGlobal = new BusinessPartnersDeployment()
            //{
            //    EternalStorageAddress = EternalStorageServiceGlobal.ContractHandler.ContractAddress
            //};
            //BusinessPartnerStorageServiceGlobal = await BusinessPartnerStorageService.DeployContractAndGetServiceAsync(
            //    _web3, bpStorageDeploymentGlobal).ConfigureAwait(false);
            //var bpStorageOwner = await BusinessPartnerStorageServiceGlobal.OwnerQueryAsync().ConfigureAwait(false);
            //Log($"{contractName} address is: {BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress}");
            //Log($"{contractName} owner is  : {bpStorageOwner}");

            //-----------------------------------------------------------------------------------
            // Configure Global Business Partner Storage
            //-----------------------------------------------------------------------------------
            // Authorisations.
            // Bind all contracts that will maintain records in BP storage here - currently none other than owner                

            // Configure Global Eternal Storage
            // Authorisations. Bind all contracts that will use global eternal storage
            Log();
            Log($"Authorisations for Global Eternal Storage...");
            contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE_GLOBAL;
            Log($"Configuring Global Eternal Storage, binding {contractName}...");
            var txReceipt = await EternalStorageServiceGlobal.BindAddressRequestAndWaitForReceiptAsync(
                BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");
        }

        private async Task DeployEShopAsync()
        {
            LogHeader($"Deploying eShopId: {ContractNewDeploymentConfig.Eshop.EShopId}, Description: {ContractNewDeploymentConfig.Eshop.EShopDescription}");
            //-----------------------------------------------------------------------------------
            // Contract deployments
            //-----------------------------------------------------------------------------------
            // Deploy Address Registry
            var contractName = CONTRACT_NAME_ADDRESS_REGISTRY_LOCAL;
            Log($"Deploying {contractName}...");
            var addressRegDeployment = new AddressRegistryDeployment();
            AddressRegistryServiceLocal = await AddressRegistryService.DeployContractAndGetServiceAsync(
                _web3, addressRegDeployment).ConfigureAwait(false);
            var addressRegOwner = await AddressRegistryServiceLocal.OwnerQueryAsync().ConfigureAwait(false);
            Log($"{contractName} address is: {AddressRegistryServiceLocal.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {addressRegOwner}");

            // Deploy Eternal Storage
            Log();
            contractName = CONTRACT_NAME_ETERNAL_STORAGE_LOCAL;
            Log($"Deploying {contractName}...");
            var eternalStorageDeployment = new EternalStorageDeployment();
            EternalStorageServiceLocal = await EternalStorageService.DeployContractAndGetServiceAsync(
                _web3, eternalStorageDeployment).ConfigureAwait(false);
            var eternalStorageOwner = await EternalStorageServiceLocal.OwnerQueryAsync().ConfigureAwait(false);
            Log($"{contractName} address is: {EternalStorageServiceLocal.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {eternalStorageOwner}");

            // Deploy PO Storage
            Log();
            contractName = CONTRACT_NAME_PO_STORAGE_LOCAL;
            Log($"Deploying {contractName}...");
            var poStorageDeployment = new PoStorageDeployment() { ContractAddressOfRegistry = AddressRegistryServiceLocal.ContractHandler.ContractAddress };
            PoStorageServiceLocal = await PoStorageService.DeployContractAndGetServiceAsync(
                _web3, poStorageDeployment).ConfigureAwait(false);
            var poStorageOwner = await PoStorageServiceLocal.OwnerQueryAsync().ConfigureAwait(false);
            Log($"{contractName} address is: {PoStorageServiceLocal.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {poStorageOwner}");

            // Deploy Buyer Wallet
            Log();
            contractName = CONTRACT_NAME_BUYER_WALLET;
            Log($"Deploying {contractName}...");
            var buyerWalletDeployment = new BuyerWalletDeployment() { BusinessPartnerStorageAddressGlobal = BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress };
            BuyerWalletService = await BuyerWalletService.DeployContractAndGetServiceAsync(
                _web3, buyerWalletDeployment).ConfigureAwait(false);
            var buyerWalletOwner = await BuyerWalletService.OwnerQueryAsync().ConfigureAwait(false);
            Log($"{contractName} address is: {BuyerWalletService.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {buyerWalletOwner}");

            // Deploy Seller Admin
            Log();
            contractName = CONTRACT_NAME_SELLER_ADMIN;
            Log($"Deploying {contractName}...");
            var sellerAdminDeployment = new SellerAdminDeployment()
            {
                BusinessPartnerStorageAddressGlobal = BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress,
                SellerIdString = ContractNewDeploymentConfig.Seller.SellerId,
            };
            SellerAdminService = await SellerAdminService.DeployContractAndGetServiceAsync(
                _web3, sellerAdminDeployment).ConfigureAwait(false);
            var sellerAdminOwner = await SellerAdminService.OwnerQueryAsync().ConfigureAwait(false);
            Log($"{contractName} address is: {SellerAdminService.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {sellerAdminOwner}");

            // Deploy Purchasing
            Log();
            contractName = CONTRACT_NAME_PURCHASING_LOCAL;
            Log($"Deploying {contractName}...");
            var purchasingDeployment = new PurchasingDeployment()
            {
                AddressRegistryLocalAddress = AddressRegistryServiceLocal.ContractHandler.ContractAddress,
                EShopIdString = ContractNewDeploymentConfig.Eshop.EShopId
            };
            PurchasingServiceLocal = await PurchasingService.DeployContractAndGetServiceAsync(
                _web3, purchasingDeployment).ConfigureAwait(false);
            var purchasingOwner = await PurchasingServiceLocal.OwnerQueryAsync().ConfigureAwait(false);
            Log($"{contractName} address is: {PurchasingServiceLocal.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {purchasingOwner}");

            // Deploy Funding
            Log();
            contractName = CONTRACT_NAME_FUNDING_LOCAL;
            Log($"Deploying {contractName}...");
            var fundingDeployment = new FundingDeployment()
            {            
                AddressRegistryLocalAddress = AddressRegistryServiceLocal.ContractHandler.ContractAddress,
            };
            FundingServiceLocal = await FundingService.DeployContractAndGetServiceAsync(
                _web3, fundingDeployment).ConfigureAwait(false);
            var fundingOwner = await FundingServiceLocal.OwnerQueryAsync().ConfigureAwait(false);
            Log($"{contractName} address is: {FundingServiceLocal.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {fundingOwner}");
            Log($"Deploy complete.");
        }

        private async Task ConfigureEShopAsync()
        {
            LogHeader($"Configuring eShopId: {ContractNewDeploymentConfig.Eshop.EShopId}, Description: {ContractNewDeploymentConfig.Eshop.EShopDescription}");

            //-----------------------------------------------------------------------------------
            // Configure Local Address Registry
            //-----------------------------------------------------------------------------------
            #region configure address registry
            Log($"Configuring Local Address Registry...");

            // Add address entry for eternal storage
            var contractName = CONTRACT_NAME_ETERNAL_STORAGE_LOCAL;
            Log($"Configuring Local Address Registry, adding {contractName}...");
            var txReceipt = await AddressRegistryServiceLocal.RegisterAddressStringRequestAndWaitForReceiptAsync(
                contractName, EternalStorageServiceLocal.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            // Add address entry for PO storage
            contractName = CONTRACT_NAME_PO_STORAGE_LOCAL;
            Log($"Configuring Local Address Registry, adding {contractName}...");
            txReceipt = await AddressRegistryServiceLocal.RegisterAddressStringRequestAndWaitForReceiptAsync(
                contractName, PoStorageServiceLocal.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            // Add address entry for Purchasing
            contractName = CONTRACT_NAME_PURCHASING_LOCAL;
            Log($"Configuring Local Address Registry, adding {contractName}...");
            txReceipt = await AddressRegistryServiceLocal.RegisterAddressStringRequestAndWaitForReceiptAsync(
                contractName, PurchasingServiceLocal.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            // Add address entry for Funding
            contractName = CONTRACT_NAME_FUNDING_LOCAL;
            Log($"Configuring Local Address Registry, adding {contractName}...");
            txReceipt = await AddressRegistryServiceLocal.RegisterAddressStringRequestAndWaitForReceiptAsync(
                contractName, FundingServiceLocal.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            // Authorisations. Nothing needed.
            #endregion

            //-----------------------------------------------------------------------------------
            // Configure Local Eternal Storage
            //-----------------------------------------------------------------------------------
            // Authorisations. Bind all contracts that will use eternal storage
            Log();
            Log($"Authorisations for Local Eternal Storage...");
            contractName = CONTRACT_NAME_PO_STORAGE_LOCAL;
            Log($"Configuring Local Eternal Storage, binding {contractName}...");
            txReceipt = await EternalStorageServiceLocal.BindAddressRequestAndWaitForReceiptAsync(
                PoStorageServiceLocal.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            //-----------------------------------------------------------------------------------
            // Configure Local PO Storage
            //-----------------------------------------------------------------------------------
            Log();
            Log($"Configuring Local PO Storage...");
            txReceipt = await PoStorageServiceLocal.ConfigureRequestAndWaitForReceiptAsync(
                CONTRACT_NAME_ETERNAL_STORAGE_LOCAL).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            // Authorisations. Bind all contracts that will use Local PO storage
            // Bind Purchasing to Local PO Storage
            Log($"Authorisations for Local PO Storage...");
            contractName = CONTRACT_NAME_PURCHASING_LOCAL;
            Log($"Configuring PO Storage, binding {contractName}...");
            txReceipt = await PoStorageServiceLocal.BindAddressRequestAndWaitForReceiptAsync(
                PurchasingServiceLocal.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            //-----------------------------------------------------------------------------------
            // Configure Purchasing
            //-----------------------------------------------------------------------------------
            Log();
            Log($"Configuring Purchasing...");
            txReceipt = await PurchasingServiceLocal.ConfigureRequestAndWaitForReceiptAsync(
                BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress,
                CONTRACT_NAME_PO_STORAGE_LOCAL,
                CONTRACT_NAME_FUNDING_LOCAL)
                .ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            // Authorisations. Bind all contracts that will use Purchasing                
            Log($"Authorisations for Purchasing...");
            // Bind BuyerWallet to Purchasing
            contractName = CONTRACT_NAME_BUYER_WALLET;
            Log($"Configuring Purchasing, binding {contractName}...");
            txReceipt = await PurchasingServiceLocal.BindAddressRequestAndWaitForReceiptAsync(
                BuyerWalletService.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");
            // Bind SellerAdmin to Purchasing
            contractName = CONTRACT_NAME_SELLER_ADMIN;
            Log($"Configuring Purchasing, binding {contractName}...");
            txReceipt = await PurchasingServiceLocal.BindAddressRequestAndWaitForReceiptAsync(
                SellerAdminService.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");
            // Bind Funding to Purchasing
            contractName = CONTRACT_NAME_FUNDING_LOCAL;
            Log($"Configuring Purchasing, binding {contractName}...");
            txReceipt = await PurchasingServiceLocal.BindAddressRequestAndWaitForReceiptAsync(
                FundingServiceLocal.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            //-----------------------------------------------------------------------------------
            // Configure Funding
            //-----------------------------------------------------------------------------------
            Log();
            Log($"Configuring Funding...");
            txReceipt = await FundingServiceLocal.ConfigureRequestAndWaitForReceiptAsync(
                BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress,
                CONTRACT_NAME_PO_STORAGE_LOCAL)
                .ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            // Authorisations. Bind all contracts that will use Funding                
            Log($"Authorisations for Funding...");
            // Bind BuyerWallet to Funding
            contractName = CONTRACT_NAME_BUYER_WALLET;
            Log($"Configuring Funding, binding {contractName}...");
            txReceipt = await FundingServiceLocal.BindAddressRequestAndWaitForReceiptAsync(
                BuyerWalletService.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");
            // Bind Purchasing to Funding
            contractName = CONTRACT_NAME_PURCHASING_LOCAL;
            Log($"Configuring Funding, binding {contractName}...");
            txReceipt = await FundingServiceLocal.BindAddressRequestAndWaitForReceiptAsync(
                PurchasingServiceLocal.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");
            Log($"Configure complete.");
        }

        private async Task DeployMockContractsAsync()
        {
            LogHeader("Deploy MockDai");
            var contractName = "MockDai";
            Log($"Deploying {contractName}...");
            try
            {
                var mockDaiDeployment = new MockDaiDeployment();
                MockDaiService = await MockDaiService.DeployContractAndGetServiceAsync(
                    _web3, mockDaiDeployment).ConfigureAwait(false);
                var mockDaiOwner = await MockDaiService.OwnerQueryAsync().ConfigureAwait(false);
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

        private void LogHeader(string s)
        {
            Log();
            Log($"--------------  {s}  --------------");
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