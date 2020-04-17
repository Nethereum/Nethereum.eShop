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
        public IEshopDeployment EshopDeployment { get; internal set; }
        public IBuyerDeployment BuyerDeployment { get; internal set; }
        public IBuyerDeployment BuyerDeployment02 { get; internal set; }
        public ISellerDeployment SellerDeployment { get; internal set; }
        public ISellerDeployment SellerDeployment02 { get; internal set; }
        public IBusinessPartnersDeployment BusinessPartnersDeployment { get; internal set; }
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
            BusinessPartnersDeployment = Deployment.BusinessPartnersDeployment.CreateFromNewDeployment(_web3, _logger);
            await BusinessPartnersDeployment.InitializeAsync().ConfigureAwait(false);

            // The shop
            EshopDeployment = Deployment.EshopDeployment.CreateFromNewDeployment(
                _web3,
                new EshopDeploymentConfig()
                {
                    BusinessPartnerStorageGlobalAddress = BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                    EshopId = _config.EshopDeploymentConfig.EshopId,
                    EshopDescription = _config.EshopDeploymentConfig.EshopDescription,
                    QuoteSigners = _config.EshopDeploymentConfig.QuoteSigners
                },
                _logger);
            await EshopDeployment.InitializeAsync().ConfigureAwait(false);

            // Buyer 1
            BuyerDeployment = Deployment.BuyerDeployment.CreateFromNewDeployment(
                _web3,
                new BuyerDeploymentConfig()
                {
                    BusinessPartnerStorageGlobalAddress = BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                },
                _logger);
            await BuyerDeployment.InitializeAsync().ConfigureAwait(false);

            // Buyer 2
            BuyerDeployment02 = Deployment.BuyerDeployment.CreateFromNewDeployment(
                _web3,
                new BuyerDeploymentConfig()
                {
                    BusinessPartnerStorageGlobalAddress = BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                },
                _logger);
            await BuyerDeployment02.InitializeAsync().ConfigureAwait(false);

            // Seller 1
            SellerDeployment = Deployment.SellerDeployment.CreateFromNewDeployment(
                _web3,
                new SellerDeploymentConfig()
                {
                    BusinessPartnerStorageGlobalAddress = BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                    SellerId = _config.SellerDeploymentConfig.SellerId,
                    SellerDescription = _config.SellerDeploymentConfig.SellerDescription,
                },
                _logger);
            await SellerDeployment.InitializeAsync().ConfigureAwait(false);

            // Seller 2
            SellerDeployment02 = Deployment.SellerDeployment.CreateFromNewDeployment(
                _web3,
                 new SellerDeploymentConfig()
                 {
                     BusinessPartnerStorageGlobalAddress = BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                     SellerId = _config.SellerDeploymentConfig02.SellerId,
                     SellerDescription = _config.SellerDeploymentConfig02.SellerDescription,
                 },
                _logger);
            await SellerDeployment02.InitializeAsync().ConfigureAwait(false);

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
