using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.MockDai;
using Nethereum.Commerce.Contracts.MockDai.ContractDefinition;
using Nethereum.Web3;
using System.Threading.Tasks;

namespace Nethereum.Commerce.Contracts.Deployment.CompleteSample
{
    /// <summary>
    /// Deploys a new complete sample Eshop along with a new global business partner
    /// storage, two buyers, two sellers and a mock DAI token.
    /// </summary>
    public class CompleteSampleDeployment : ContractDeploymentBase
    {
        public IEshopDeployment Eshop { get; internal set; }
        public IBuyerDeployment Buyer { get; internal set; }
        public IBuyerDeployment Buyer02 { get; internal set; }
        public ISellerDeployment Seller { get; internal set; }
        public ISellerDeployment Seller02 { get; internal set; }
        public IBusinessPartnersDeployment BusinessPartners { get; internal set; }
        public MockDaiService MockDaiService { get; internal set; }

        private CompleteSampleDeploymentConfig _config;

        /// <summary>
        /// Deploy and configure a complete sample deployment
        /// </summary>
        public static CompleteSampleDeployment CreateFromNewDeployment(
            IWeb3 web3,
            CompleteSampleDeploymentConfig completeSampleDeploymentConfig,
            ILogger logger = null)
        {
            return new CompleteSampleDeployment(web3, completeSampleDeploymentConfig, true, logger);
        }

        private CompleteSampleDeployment(
            IWeb3 web3,
            CompleteSampleDeploymentConfig completeSampleDeploymentConfig,
            bool isNewDeployment,
            ILogger logger = null)
            : base(web3, logger)
        {
            _config = completeSampleDeploymentConfig;
            _isNewDeployment = isNewDeployment;
        }

        public async Task InitializeAsync()
        {
            // Global business partners storage
            BusinessPartners = Deployment.BusinessPartnersDeployment.CreateFromNewDeployment(_web3, _logger);
            await BusinessPartners.InitializeAsync().ConfigureAwait(false);

            // The shop
            Eshop = Deployment.EshopDeployment.CreateFromNewDeployment(
                _web3,
                new EshopDeploymentConfig()
                {
                    BusinessPartnerStorageGlobalAddress = BusinessPartners.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                    EshopId = _config.Eshop.EshopId,
                    EshopDescription = _config.Eshop.EshopDescription,
                    QuoteSigners = _config.Eshop.QuoteSigners
                },
                _logger);
            await Eshop.InitializeAsync().ConfigureAwait(false);

            // Buyer 1
            Buyer = Deployment.BuyerDeployment.CreateFromNewDeployment(
                _web3,
                new BuyerDeploymentConfig()
                {
                    BusinessPartnerStorageGlobalAddress = BusinessPartners.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                },
                _logger);
            await Buyer.InitializeAsync().ConfigureAwait(false);

            // Buyer 2
            Buyer02 = Deployment.BuyerDeployment.CreateFromNewDeployment(
                _web3,
                new BuyerDeploymentConfig()
                {
                    BusinessPartnerStorageGlobalAddress = BusinessPartners.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                },
                _logger);
            await Buyer02.InitializeAsync().ConfigureAwait(false);

            // Seller 1
            Seller = Deployment.SellerDeployment.CreateFromNewDeployment(
                _web3,
                new SellerDeploymentConfig()
                {
                    BusinessPartnerStorageGlobalAddress = BusinessPartners.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                    SellerId = _config.Seller.SellerId,
                    SellerDescription = _config.Seller.SellerDescription,
                },
                _logger);
            await Seller.InitializeAsync().ConfigureAwait(false);

            // Seller 2
            Seller02 = Deployment.SellerDeployment.CreateFromNewDeployment(
                _web3,
                 new SellerDeploymentConfig()
                 {
                     BusinessPartnerStorageGlobalAddress = BusinessPartners.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                     SellerId = _config.Seller02.SellerId,
                     SellerDescription = _config.Seller02.SellerDescription,
                 },
                _logger);
            await Seller02.InitializeAsync().ConfigureAwait(false);

            // Configure sellers as sellers for the shop (Sellers must whitelist shop as 
            // being allowed to send them events)

            // Authorisations - SellerAdmin must register the eShop, adding the eShop purchasing contract to its whitelist
            Log();
            Log($"Configuring Seller Admin, binding Purchasing...");
            var txReceipt = await Seller.SellerAdminService.BindAddressRequestAndWaitForReceiptAsync(
                Eshop.PurchasingService.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            Log($"Configuring Seller Admin 02, binding Purchasing...");
            txReceipt = await Seller02.SellerAdminService.BindAddressRequestAndWaitForReceiptAsync(
                Eshop.PurchasingService.ContractHandler.ContractAddress).ConfigureAwait(false);
            Log($"Tx status: {txReceipt.Status.Value}");

            // Some DAI
            var mockDaiDeployment = new MockDaiDeployment();
            MockDaiService = await MockDaiService.DeployContractAndGetServiceAsync(
                _web3, mockDaiDeployment).ConfigureAwait(false);
            var mockDaiOwner = await MockDaiService.OwnerQueryAsync().ConfigureAwait(false);
            Log($"DAI address is: {MockDaiService.ContractHandler.ContractAddress}");
            Log($"DAI owner is  : {mockDaiOwner}");
        }
    }
}
