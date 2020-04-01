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
using System.IO;
using Newtonsoft.Json;

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
        public BuyerWalletService BuyerWalletService { get; internal set; }
        public BuyerWalletService BuyerWalletService02 { get; internal set; }
        public SellerAdminService SellerAdminService { get; internal set; }
        public PurchasingService PurchasingService { get; internal set; }
        public FundingService FundingService { get; internal set; }

        // Mocks
        public MockDaiService MockDaiService { get; internal set; }

        // Configuration
        public readonly ContractNewDeploymentConfig ContractNewDeploymentConfig;
        public readonly ContractConnectExistingConfig ContractConnectExistingConfig;

        // Contract names used internally by eg address registry
        public const string CONTRACT_NAME_ADDRESS_REGISTRY = "AddressRegistry";
        public const string CONTRACT_NAME_ETERNAL_STORAGE = "EternalStorage";
        public const string CONTRACT_NAME_BUSINESS_PARTNER_STORAGE = "BusinessPartnerStorage";
        public const string CONTRACT_NAME_PO_STORAGE = "PoStorage";
        public const string CONTRACT_NAME_BUYER_WALLET = "BuyerWallet";
        public const string CONTRACT_NAME_SELLER_ADMIN = "SellerAdmin";
        public const string CONTRACT_NAME_PURCHASING = "Purchasing";
        public const string CONTRACT_NAME_FUNDING = "Funding";

        private Web3.Web3 _web3;
        private ILogger _logger;
        private readonly bool _isToConnectToExistingDeployment;

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
            _isToConnectToExistingDeployment = false;
        }

        /// <summary>
        /// Connect to an existing set of eShop contracts
        /// </summary>        
        public ContractDeployment(IWeb3 web3, ContractConnectExistingConfig contractConnectExistingConfig, ILogger logger = null)
        {
            // TODO this needs updated to check new fields
            Guard.Against.Null(web3, nameof(web3));
            Guard.Against.NullOrWhiteSpace(contractConnectExistingConfig.BuyerWalletAddress, nameof(contractConnectExistingConfig.BuyerWalletAddress));
            Guard.Against.NullOrWhiteSpace(contractConnectExistingConfig.SellerAdminAddress, nameof(contractConnectExistingConfig.SellerAdminAddress));
            // eg Guard.Against.NullOrWhiteSpace(cdc.Eshop.PurchasingContractAddress, nameof(cdc.Eshop.PurchasingContractAddress));
            _web3 = (Web3.Web3)web3; // code-genned classes require web3, not an iweb3
            ContractConnectExistingConfig = contractConnectExistingConfig;
            _logger = logger;
            _isToConnectToExistingDeployment = true;
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

            if (_isToConnectToExistingDeployment)
            {
                // Connect to an existing deployment
                await ConnectToAnExistingDeploymentAsync().ConfigureAwait(false);
            }
            else
            {
                // Make a whole new deployment
                await DeployAndConfigureEShopAsync().ConfigureAwait(false);

                await WriteContractAddressesToFile().ConfigureAwait(false);

                // With mocks if needed
                if (ContractNewDeploymentConfig.AlsoDeployMockContracts)
                {
                    await DeployMockContractsAsync().ConfigureAwait(false);
                }
            }
            LogSeparator();

            // End meaurements
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

            LogSeparator();
        }

        // A temporary method which writes the addresses created by the deployment to a file
        // This can be used by the web job project for testing
        private Task WriteContractAddressesToFile()
        {
            var configJson = new
            {
                Web3User = _web3.TransactionManager.Account.Address.ToLowerInvariant(),
                ContractNameFunding = FundingService.ContractHandler.ContractAddress.ToLowerInvariant(),
                SellerAdminOwner = SellerAdminService.ContractHandler.ContractAddress.ToLowerInvariant(),
                AddressRegistry = AddressRegistryService.ContractHandler.ContractAddress.ToLowerInvariant(),
                PurchasingContract = PurchasingService.ContractHandler.ContractAddress.ToLowerInvariant(),
                BusinessPartnerStorage = BusinessPartnerStorageService.ContractHandler.ContractAddress.ToLowerInvariant(),
                BuyerUserAddress = _web3.TransactionManager.Account.Address.ToLowerInvariant(),
                BuyerReceiverAddress = _web3.TransactionManager.Account.Address.ToLowerInvariant(),
                BuyerWalletAddress = BuyerWalletService.ContractHandler.ContractAddress.ToLowerInvariant(),
                CurrencyAddress = MockDaiService.ContractHandler.ContractAddress.ToLowerInvariant(),
                EShopId = ContractNewDeploymentConfig.Eshop.EShopId,
                SellerId = ContractNewDeploymentConfig.Seller.SellerId,
                CurrencySymbol = "DAI",
                QuoteSigners = new[] { "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9", "0x94618601FE6cb8912b274E5a00453949A57f8C1e" }
            };

            File.WriteAllText("c:/temp/eshop_contracts.json", JsonConvert.SerializeObject(configJson));

            return Task.CompletedTask;
        }

        private async Task DeployAndConfigureEShopAsync()
        {
            try
            {
                Log($"Deploying eShop: {ContractNewDeploymentConfig.Eshop.EShopId} {ContractNewDeploymentConfig.Eshop.EShopDescription}...");

                //-----------------------------------------------------------------------------------
                // Contract deployments
                //-----------------------------------------------------------------------------------
                #region contract deployments
                // Deploy Address Registry
                Log();
                var contractName = CONTRACT_NAME_ADDRESS_REGISTRY;
                Log($"Deploying {contractName}...");
                var addressRegDeployment = new AddressRegistryDeployment();
                AddressRegistryService = await AddressRegistryService.DeployContractAndGetServiceAsync(
                    _web3, addressRegDeployment).ConfigureAwait(false);
                var addressRegOwner = await AddressRegistryService.OwnerQueryAsync().ConfigureAwait(false);
                Log($"{contractName} address is: {AddressRegistryService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {addressRegOwner}");

                // Deploy Eternal Storage
                Log();
                contractName = CONTRACT_NAME_ETERNAL_STORAGE;
                Log($"Deploying {contractName}...");
                var eternalStorageDeployment = new EternalStorageDeployment();
                EternalStorageService = await EternalStorageService.DeployContractAndGetServiceAsync(
                    _web3, eternalStorageDeployment).ConfigureAwait(false);
                var eternalStorageOwner = await EternalStorageService.OwnerQueryAsync().ConfigureAwait(false);
                Log($"{contractName} address is: {EternalStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {eternalStorageOwner}");

                // Deploy Business Partner Storage
                Log();
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                Log($"Deploying {contractName}...");
                var bpStorageDeployment = new BusinessPartnerStorageDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                BusinessPartnerStorageService = await BusinessPartnerStorageService.DeployContractAndGetServiceAsync(
                    _web3, bpStorageDeployment).ConfigureAwait(false);
                var bpStorageOwner = await BusinessPartnerStorageService.OwnerQueryAsync().ConfigureAwait(false);
                Log($"{contractName} address is: {BusinessPartnerStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {bpStorageOwner}");

                // Deploy PO Storage
                Log();
                contractName = CONTRACT_NAME_PO_STORAGE;
                Log($"Deploying {contractName}...");
                var poStorageDeployment = new PoStorageDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                PoStorageService = await PoStorageService.DeployContractAndGetServiceAsync(
                    _web3, poStorageDeployment).ConfigureAwait(false);
                var poStorageOwner = await PoStorageService.OwnerQueryAsync().ConfigureAwait(false);
                Log($"{contractName} address is: {PoStorageService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {poStorageOwner}");

                // Deploy Wallet Buyer
                Log();
                contractName = CONTRACT_NAME_BUYER_WALLET;
                Log($"Deploying {contractName}...");
                var buyerWalletDeployment = new BuyerWalletDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                BuyerWalletService = await BuyerWalletService.DeployContractAndGetServiceAsync(
                    _web3, buyerWalletDeployment).ConfigureAwait(false);
                var buyerWalletOwner = await BuyerWalletService.OwnerQueryAsync().ConfigureAwait(false);
                Log($"{contractName} address is: {BuyerWalletService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {buyerWalletOwner}");

                // Deploy Wallet Seller
                Log();
                contractName = CONTRACT_NAME_SELLER_ADMIN;
                Log($"Deploying {contractName}...");
                var sellerAdminDeployment = new SellerAdminDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                SellerAdminService = await SellerAdminService.DeployContractAndGetServiceAsync(
                    _web3, sellerAdminDeployment).ConfigureAwait(false);
                var sellerAdminOwner = await SellerAdminService.OwnerQueryAsync().ConfigureAwait(false);
                Log($"{contractName} address is: {SellerAdminService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {sellerAdminOwner}");

                // Deploy Purchasing
                Log();
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Deploying {contractName}...");
                var purchasingDeployment = new PurchasingDeployment()
                {
                    ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress,
                    EShopIdString = ContractNewDeploymentConfig.Eshop.EShopId
                };
                PurchasingService = await PurchasingService.DeployContractAndGetServiceAsync(
                    _web3, purchasingDeployment).ConfigureAwait(false);
                var purchasingOwner = await PurchasingService.OwnerQueryAsync().ConfigureAwait(false);
                Log($"{contractName} address is: {PurchasingService.ContractHandler.ContractAddress}");
                Log($"{contractName} owner is  : {purchasingOwner}");

                // Deploy Funding
                Log();
                contractName = CONTRACT_NAME_FUNDING;
                Log($"Deploying {contractName}...");
                var fundingDeployment = new FundingDeployment() { ContractAddressOfRegistry = AddressRegistryService.ContractHandler.ContractAddress };
                FundingService = await FundingService.DeployContractAndGetServiceAsync(
                    _web3, fundingDeployment).ConfigureAwait(false);
                var fundingOwner = await FundingService.OwnerQueryAsync().ConfigureAwait(false);
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
                var txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(
                    contractName, EternalStorageService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for BP storage 
                contractName = CONTRACT_NAME_BUSINESS_PARTNER_STORAGE;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(
                    contractName, BusinessPartnerStorageService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for PO storage
                contractName = CONTRACT_NAME_PO_STORAGE;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(
                    contractName, PoStorageService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for Purchasing
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(
                    contractName, PurchasingService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Add address entry for Funding
                contractName = CONTRACT_NAME_FUNDING;
                Log($"Configuring Address Registry, adding {contractName}...");
                txReceipt = await AddressRegistryService.RegisterAddressStringRequestAndWaitForReceiptAsync(
                    contractName, FundingService.ContractHandler.ContractAddress).ConfigureAwait(false);
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
                txReceipt = await EternalStorageService.BindAddressRequestAndWaitForReceiptAsync(
                    BusinessPartnerStorageService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                contractName = CONTRACT_NAME_PO_STORAGE;
                Log($"Configuring Eternal Storage, binding {contractName}...");
                txReceipt = await EternalStorageService.BindAddressRequestAndWaitForReceiptAsync(
                    PoStorageService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");
                #endregion

                //-----------------------------------------------------------------------------------
                // Configure Business Partner Storage
                //-----------------------------------------------------------------------------------
                #region configure business partner storage and store master data
                Log();
                Log($"Configuring BP Storage...");
                txReceipt = await BusinessPartnerStorageService.ConfigureRequestAndWaitForReceiptAsync(
                    CONTRACT_NAME_ETERNAL_STORAGE).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------                                
                // Add some Business Partner master data
                //-----------------------------------------------------------------------------------
                // Need at least one eShop and one Seller to be a useful deployment
                Log($"Adding eShop master data...");
                txReceipt = await BusinessPartnerStorageService.SetEshopRequestAndWaitForReceiptAsync(
                    new Eshop()
                    {
                        EShopId = ContractNewDeploymentConfig.Eshop.EShopId,
                        EShopDescription = ContractNewDeploymentConfig.Eshop.EShopDescription,
                        PurchasingContractAddress = PurchasingService.ContractHandler.ContractAddress,
                        IsActive = true,
                        CreatedByAddress = string.Empty,  // filled by contract
                        QuoteSignerCount = Convert.ToUInt32(ContractNewDeploymentConfig.Eshop.QuoteSigners.Count),
                        QuoteSigners = ContractNewDeploymentConfig.Eshop.QuoteSigners
                    }).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                Log($"Adding Seller master data...");
                txReceipt = await BusinessPartnerStorageService.SetSellerRequestAndWaitForReceiptAsync(
                    new Seller()
                    {
                        SellerId = ContractNewDeploymentConfig.Seller.SellerId,
                        SellerDescription = ContractNewDeploymentConfig.Seller.SellerDescription,
                        AdminContractAddress = SellerAdminService.ContractHandler.ContractAddress,
                        IsActive = true,
                        CreatedByAddress = string.Empty  // filled by contract
                    }).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");
                #endregion

                // Authorisations.
                // Bind all contracts that will use BP storage here - currently none other than owner                

                //-----------------------------------------------------------------------------------
                // Configure PO Storage
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring PO Storage...");
                txReceipt = await PoStorageService.ConfigureRequestAndWaitForReceiptAsync(
                    CONTRACT_NAME_ETERNAL_STORAGE).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Authorisations. Bind all contracts that will use PO storage
                // Bind Purchasing to PO Storage
                Log($"Authorisations for PO Storage...");
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Configuring PO Storage, binding {contractName}...");
                txReceipt = await PoStorageService.BindAddressRequestAndWaitForReceiptAsync(
                    PurchasingService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Wallet Seller
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring Wallet Seller...");
                txReceipt = await SellerAdminService.ConfigureRequestAndWaitForReceiptAsync(
                    ContractNewDeploymentConfig.Seller.SellerId, CONTRACT_NAME_BUSINESS_PARTNER_STORAGE)
                    .ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Wallet Buyer
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring Wallet Buyer...");
                txReceipt = await BuyerWalletService.ConfigureRequestAndWaitForReceiptAsync(
                    CONTRACT_NAME_BUSINESS_PARTNER_STORAGE).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Purchasing
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring Purchasing...");
                txReceipt = await PurchasingService.ConfigureRequestAndWaitForReceiptAsync(
                    CONTRACT_NAME_PO_STORAGE, CONTRACT_NAME_BUSINESS_PARTNER_STORAGE, CONTRACT_NAME_FUNDING)
                    .ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Authorisations. Bind all contracts that will use Purchasing                
                Log($"Authorisations for Purchasing...");
                // Bind BuyerWallet to Purchasing
                contractName = CONTRACT_NAME_BUYER_WALLET;
                Log($"Configuring Purchasing, binding {contractName}...");
                txReceipt = await PurchasingService.BindAddressRequestAndWaitForReceiptAsync(
                    BuyerWalletService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");
                // Bind SellerAdmin to Purchasing
                contractName = CONTRACT_NAME_SELLER_ADMIN;
                Log($"Configuring Purchasing, binding {contractName}...");
                txReceipt = await PurchasingService.BindAddressRequestAndWaitForReceiptAsync(
                    SellerAdminService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");
                // Bind Funding to Purchasing
                contractName = CONTRACT_NAME_FUNDING;
                Log($"Configuring Purchasing, binding {contractName}...");
                txReceipt = await PurchasingService.BindAddressRequestAndWaitForReceiptAsync(
                    FundingService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                //-----------------------------------------------------------------------------------
                // Configure Funding
                //-----------------------------------------------------------------------------------
                Log();
                Log($"Configuring Funding...");
                txReceipt = await FundingService.ConfigureRequestAndWaitForReceiptAsync(
                    CONTRACT_NAME_PO_STORAGE, CONTRACT_NAME_BUSINESS_PARTNER_STORAGE)
                    .ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");

                // Authorisations. Bind all contracts that will use Funding                
                Log($"Authorisations for Funding...");
                // Bind BuyerWallet to Funding
                contractName = CONTRACT_NAME_BUYER_WALLET;
                Log($"Configuring Funding, binding {contractName}...");
                txReceipt = await FundingService.BindAddressRequestAndWaitForReceiptAsync(
                    BuyerWalletService.ContractHandler.ContractAddress).ConfigureAwait(false);
                Log($"Tx status: {txReceipt.Status.Value}");
                // Bind Purchasing to Funding
                contractName = CONTRACT_NAME_PURCHASING;
                Log($"Configuring Funding, binding {contractName}...");
                txReceipt = await FundingService.BindAddressRequestAndWaitForReceiptAsync(
                    PurchasingService.ContractHandler.ContractAddress).ConfigureAwait(false);
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
