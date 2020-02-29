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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;

namespace Nethereum.Commerce.Contracts
{
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

        // Contract names used internally by eg address registry
        public const string CONTRACT_NAME_ADDRESS_REGISTRY = "AddressRegistry";
        public const string CONTRACT_NAME_ETERNAL_STORAGE = "EternalStorage";
        public const string CONTRACT_NAME_BUSINESS_PARTNER_STORAGE = "BusinessPartnerStorage";
        public const string CONTRACT_NAME_PO_STORAGE = "PoStorage";
        public const string CONTRACT_NAME_WALLET_BUYER = "WalletBuyer";
        public const string CONTRACT_NAME_WALLET_SELLER = "WalletSeller";
        public const string CONTRACT_NAME_PURCHASING = "Purchasing";
        public const string CONTRACT_NAME_FUNDING = "Funding";

        private ILogger _logger;

        public ContractDeployment(ContractDeploymentConfig contractDeploymentConfig, ILogger logger = null)
        {
            Guard.Against.NullOrWhiteSpace(contractDeploymentConfig.BlockchainUrl, nameof(contractDeploymentConfig.BlockchainUrl));
            Guard.Against.NullOrWhiteSpace(contractDeploymentConfig.EShopSellerId, nameof(contractDeploymentConfig.EShopSellerId));
            Guard.Against.NullOrWhiteSpace(contractDeploymentConfig.EShopDescription, nameof(contractDeploymentConfig.EShopDescription));
            Guard.Against.NullOrWhiteSpace(contractDeploymentConfig.EShopApproverAddress, nameof(contractDeploymentConfig.EShopApproverAddress));
            Guard.Against.NullOrWhiteSpace(contractDeploymentConfig.BlockchainUrl, nameof(contractDeploymentConfig.ContractDeploymentOwnerPrivateKey));
            ContractDeploymentConfig = contractDeploymentConfig;

            _logger = logger;
        }

        public void Deploy()
        {
            //try
            {
                //Log($"Deploying eShop: {Deployment.ContractDeploymentConfig.EShopSellerId} {Deployment.ContractDeploymentConfig.EShopDescription}...");
                //var url = Deployment.ContractDeploymentConfig.BlockchainUrl;
                //var privateKey = Deployment.ContractDeploymentConfig.ContractDeploymentOwnerPrivateKey;
                //var account = new Account(privateKey);
                //Web3 = new Web3.Web3(account, url);
                // etc
            }
        }

        private void Log(string message)
        {
            if (_logger != null)
            {
                _logger.LogInformation(message);
            }
        }
    }
}
