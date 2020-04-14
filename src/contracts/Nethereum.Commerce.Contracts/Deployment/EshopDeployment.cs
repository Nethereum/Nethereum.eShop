using Microsoft.Extensions.Logging;
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
using Nethereum.Commerce.Contracts.SellerAdmin;
using Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.Web3;
using System.Linq;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment
{
    public class EshopDeployment : ContractDeploymentBase
    {
        public PurchasingService PurchasingServiceLocal { get; internal set; }
        public BusinessPartnerStorageService BusinessPartnerStorageGlobalService { get; internal set; }
        public AddressRegistryService AddressRegistryServiceLocal { get; internal set; }
        public EternalStorageService EternalStorageServiceLocal { get; internal set; }
        public PoStorageService PoStorageServiceLocal { get; internal set; }
        public FundingService FundingServiceLocal { get; internal set; }

        public string EshopId { get; internal set; }
        public string Owner { get; internal set; }

        private const string CONTRACT_NAME_ADDRESS_REGISTRY_LOCAL = "AddressRegistryLocal";
        private const string CONTRACT_NAME_ETERNAL_STORAGE_LOCAL = "EternalStorageLocal";
        private const string CONTRACT_NAME_PO_STORAGE_LOCAL = "PoStorageLocal";
        private const string CONTRACT_NAME_PURCHASING_LOCAL = "PurchasingLocal";
        private const string CONTRACT_NAME_FUNDING_LOCAL = "FundingLocal";

        private readonly string _businessPartnerStorageAddressGlobal;
        private readonly string _existingPurchasingContractAddress;
        private readonly string _eshopIdDesired;
        private readonly string _eshopDescriptionDesired;

        /// <summary>
        /// Deploy and configure a new Purchasing.sol contract, together with its Eternal Storage,
        /// PO storage and Funding contracts.
        /// </summary>
        public static EshopDeployment CreateFromNewDeployment(IWeb3 web3, string businessPartnerStorageAddressGlobal, string eshopId, string eshopDescription = null, ILogger logger = null)
        {
            return new EshopDeployment(web3, businessPartnerStorageAddressGlobal, eshopId, eshopDescription, true, logger);
        }

        /// <summary>
        /// Connect to an existing Purchasing.sol contract
        /// </summary>
        public static EshopDeployment CreateFromConnectExistingContract(IWeb3 web3, string existingPurchasingContractAddress, ILogger logger = null)
        {
            return new EshopDeployment(web3, existingPurchasingContractAddress, null, null, false, logger);
        }

        private EshopDeployment(
            IWeb3 web3,
            string address,
            string eshopId,
            string eshopDescription,
            bool isNewDeployment,
            ILogger logger = null)
            : base(web3, logger)
        {
            if (!address.IsValidNonZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {GetType().Name}. Address {address} is zero or not valid hex format.");
            }
            _eshopIdDesired = eshopId;
            _eshopDescriptionDesired = eshopDescription ?? eshopId;
            _isNewDeployment = isNewDeployment;
            if (_isNewDeployment)
            {
                // address represents the business partner storage address
                _businessPartnerStorageAddressGlobal = address;
            }
            else
            {
                // address represents the existing purchasing address
                _existingPurchasingContractAddress = address;
            }
        }

        public async Task InitializeAsync()
        {
            var contractName = "Purchasing";

            // Validate Global Business Partner Storage and get the service for it
            string bpStorageAddress;
            if (_isNewDeployment)
            {
                bpStorageAddress = _businessPartnerStorageAddressGlobal;
            }
            else
            {
                bpStorageAddress = await PurchasingServiceLocal.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            }
            BusinessPartnerStorageGlobalService = await GetValidBusinessPartnerStorageServiceAsync(bpStorageAddress).ConfigureAwait(false);

            // Do deployment or connect existing
            if (_isNewDeployment)
            {
                await DeployEshopAsync().ConfigureAwait(false);
                await AddGlobalStorageMasterDataAsync().ConfigureAwait(false);
                await ConfigureEShopAsync().ConfigureAwait(false);
            }
            else
            {
                LogHeader($"Connecting to existing {contractName}...");
                PurchasingServiceLocal = new PurchasingService(_web3, _existingPurchasingContractAddress);
            }



            // Check Purchasing owner address
            var sellerAdminOwner = await PurchasingServiceLocal.OwnerQueryAsync().ConfigureAwait(false);
            if (sellerAdminOwner.IsValidNonZeroAddress())
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. Owner address must have a value.");
            }
            Log($"{contractName} address is: {PurchasingServiceLocal.ContractHandler.ContractAddress}");
            Log($"{contractName} owner is  : {sellerAdminOwner}");
            Owner = sellerAdminOwner;

            // Check EshopId string
            EshopId = (await PurchasingServiceLocal.EShopIdQueryAsync().ConfigureAwait(false)).ConvertToString();
            if (string.IsNullOrWhiteSpace(EshopId))
            {
                throw new ContractDeploymentException($"Failed to set up {contractName}. EshopId must have a value.");
            }
            Log("Done");
        }

        private async Task DeployEshopAsync()
        {
            LogHeader($"Deploying eShopId: {_eshopIdDesired}, Description: {_eshopDescriptionDesired}");
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

            // Deploy Purchasing
            Log();
            contractName = CONTRACT_NAME_PURCHASING_LOCAL;
            Log($"Deploying {contractName}...");
            var purchasingDeployment = new PurchasingDeployment()
            {
                AddressRegistryLocalAddress = AddressRegistryServiceLocal.ContractHandler.ContractAddress,
                EShopIdString = _eshopIdDesired
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

        private async Task AddGlobalStorageMasterDataAsync()
        {
            Log($"Add eShop master data...");
            var txReceipt = await BusinessPartnerStorageGlobalService.SetEshopRequestAndWaitForReceiptAsync(
                new Eshop()
                {
                    EShopId = _eshopIdDesired,
                    EShopDescription = _eshopDescriptionDesired,
                    PurchasingContractAddress = PurchasingServiceLocal.ContractHandler.ContractAddress,
                    IsActive = true,
                    CreatedByAddress = string.Empty,  // filled by contract
                    //QuoteSignerCount = Convert.ToUInt32(ContractNewDeploymentConfig.Eshop.QuoteSigners.Count),
                    //QuoteSigners = ContractNewDeploymentConfig.Eshop.QuoteSigners
                }).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");


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
    }
}
