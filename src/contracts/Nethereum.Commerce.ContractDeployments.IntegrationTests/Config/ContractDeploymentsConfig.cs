namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class ContractDeploymentsConfig
    {
        /// <summary>
        /// eg , http://localhost:8545/, https://rinkeby.infura.io/v3/<key>
        /// </summary>
        public string BlockchainUrl { get; set; }

        /// <summary>
        /// eShop system id, 32 chars max, eg "Nethereum.eShop"
        /// </summary>
        public string EShopSystemId { get; set; }

        /// <summary>
        /// eShop description, eg "Satoshi's Books"
        /// </summary>
        public string EShopDescription { get; set; }

        /// <summary>
        /// Private key of the owner of the contracts that will be deployed
        /// </summary>
        public string ContractDeploymentOwnerPrivateKey { get; set; }

    }
}
