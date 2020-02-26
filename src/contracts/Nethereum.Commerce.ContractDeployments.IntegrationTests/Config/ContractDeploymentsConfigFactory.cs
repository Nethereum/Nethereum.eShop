using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class ContractDeploymentsConfigFactory
    {
        public static ContractDeploymentsConfig Get(IConfigurationRoot config)
        {
            var configDict = config.GetSection("NewDeployments").GetChildren().ToDictionary(x => x.Key, x => x.Value);

            ContractDeploymentsConfig deploymentConfig = new ContractDeploymentsConfig();
            deploymentConfig.BlockchainUrl = ConfigurationUtils.GetOrThrow(configDict, "BlockchainUrl");
            deploymentConfig.EShopSellerId = ConfigurationUtils.GetOrThrow(configDict, "EShopSystemId");
            deploymentConfig.EShopDescription = ConfigurationUtils.GetOrThrow(configDict, "EShopDescription");
            deploymentConfig.EShopApproverAddress = ConfigurationUtils.GetOrThrow(configDict, "EShopApproverAddress").ToLowerInvariant(); ;
            deploymentConfig.ContractDeploymentOwnerPrivateKey = ConfigurationUtils.GetOrThrow(configDict, "ContractDeploymentOwnerPrivateKey");
            return deploymentConfig;
        }
    }
}
