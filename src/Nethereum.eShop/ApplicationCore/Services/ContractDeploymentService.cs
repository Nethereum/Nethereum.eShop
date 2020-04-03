using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts.Deployment;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.Web3.Accounts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nethereum.eShop.ApplicationCore.Services
{
    public class ContractDeploymentService : IContractDeploymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ISettingRepository _settingRepository;

        public ContractDeploymentService(IConfiguration configuration, ISettingRepository settingRepository)
        {
            _configuration = configuration;
            _settingRepository = settingRepository;
        }

        public async Task EnsureDeployedAsync(ILoggerFactory loggerFactory, CancellationToken cancellationToken = default)
        {
            var logger = loggerFactory.CreateLogger<ContractDeploymentService>();

            var dbBasedConfig = await _settingRepository.GetEShopConfigurationSettingsAsync().ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(dbBasedConfig.BuyerWalletAddress) == false) return;

            if (string.IsNullOrEmpty(dbBasedConfig.EShop.Id))
            {
                dbBasedConfig.EShop.Id = "Nethereum.EShop";
                dbBasedConfig.EShop.Description = "Nethereum EShop";
            }

            if (string.IsNullOrEmpty(dbBasedConfig.Seller.Id))
            {
                dbBasedConfig.Seller.Id = "Nethereum.EShop.Seller.1";
                dbBasedConfig.Seller.Description = "Nethereum.EShop Seller One";
            }

            if (string.IsNullOrEmpty(dbBasedConfig.CurrencySymbol))
            {
                dbBasedConfig.CurrencySymbol = "DAI";
            }

            if(dbBasedConfig.EShop.QuoteSigners?.Length == 0)
            {
                //TODO: remove this hardcoded stuff
                dbBasedConfig.EShop.QuoteSigners = new[] { "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9", "0x94618601FE6cb8912b274E5a00453949A57f8C1e" };
            }

            if(dbBasedConfig.ProcessPurchaseOrderEvents.TimeoutMs < 1)
            {
                dbBasedConfig.ProcessPurchaseOrderEvents.TimeoutMs = 3600000;
            }

            if(dbBasedConfig.ProcessPurchaseOrderEvents.NumberOfBlocksPerBatch < 1)
            {
                dbBasedConfig.ProcessPurchaseOrderEvents.NumberOfBlocksPerBatch = 100;
            }

            if(dbBasedConfig.ProcessPurchaseOrderEvents.MinimumBlockConfirmations < 1)
            {
                dbBasedConfig.ProcessPurchaseOrderEvents.MinimumBlockConfirmations = 12;
            }

            if (string.IsNullOrEmpty(dbBasedConfig.ProcessPurchaseOrderEvents.BlockProgressJsonFile))
            {
                dbBasedConfig.ProcessPurchaseOrderEvents.BlockProgressJsonFile = "c:/temp/po_blockprogress.json";
            }

            var url = _configuration["EthereumRpcUrl"];
            var privateKey = _configuration["AccountPrivateKey"];

            var web3 = new Web3.Web3(new Account(privateKey), url);

            var contractDeploymentConfig = new ContractNewDeploymentConfig
            {
                AlsoDeployMockContracts = true,
                Eshop = new Commerce.Contracts.BusinessPartnerStorage.ContractDefinition.Eshop
                {
                    EShopId = dbBasedConfig.EShop.Id,
                    EShopDescription = dbBasedConfig.EShop.Description,
                    QuoteSigners = dbBasedConfig.EShop.QuoteSigners.ToList()
                },
                Seller = new Commerce.Contracts.BusinessPartnerStorage.ContractDefinition.Seller
                {
                    SellerId = dbBasedConfig.Seller.Id,
                    SellerDescription = dbBasedConfig.Seller.Description,
                    AdminContractAddress = dbBasedConfig.Seller.AdminContractAddress
                }                
            };

            var contractDeployment = new ContractDeployment(web3, contractDeploymentConfig, logger);

            await contractDeployment.InitializeAsync().ConfigureAwait(false);

            dbBasedConfig.BuyerWalletAddress = contractDeployment.BuyerWalletService.ContractHandler.ContractAddress;
            dbBasedConfig.PurchasingContractAddress = contractDeployment.PurchasingService.ContractHandler.ContractAddress;
            dbBasedConfig.CurrencyAddress = contractDeployment.MockDaiService.ContractHandler.ContractAddress;

            dbBasedConfig.AddressRegistryAddress = contractDeployment.AddressRegistryService?.ContractHandler?.ContractAddress;
            dbBasedConfig.FundingAddress = contractDeployment.FundingService?.ContractHandler?.ContractAddress;
            dbBasedConfig.PoStorageAddress = contractDeployment.PoStorageService?.ContractHandler?.ContractAddress;
            dbBasedConfig.BusinessPartnerStorageAddress = contractDeployment.BusinessPartnerStorageService?.ContractHandler?.ContractAddress;
            dbBasedConfig.SellerAdminAddress = contractDeployment.SellerAdminService?.ContractHandler?.ContractAddress;

            // if deployment has succeeded, presume it's ok to enable web jobs
            dbBasedConfig.CreateFakePurchaseOrdersJob.Enabled = true;
            dbBasedConfig.ProcessPurchaseOrderEvents.Enabled = true;

            await _settingRepository.UpdateAsync(dbBasedConfig);

            await _settingRepository.UnitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
