namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    public class ContractDeploymentsConfig
    {
        /// <summary>
        /// eg , http://localhost:8545/, https://rinkeby.infura.io/v3/<key>
        /// </summary>
        public string BlockchainUrl { get; set; }

        /// <summary>
        /// eShop seller id, 32 chars max, eg "Nethereum.eShop"
        /// </summary>
        public string EShopSellerId { get; set; }

        /// <summary>
        /// eShop description, eg "Satoshi's Books"
        /// </summary>
        public string EShopDescription { get; set; }

        /// <summary>
        /// EoA or contract address, the shop owner, where money sent after a sale
        /// </summary>
        public string EShopFinanceAddress { get; set; }

        /// <summary>
        /// EoA or contract address, the signer who can a sign a quotation tx to prove shop approves it
        /// </summary>
        public string EShopApproverAddress { get; set; }
        
        /// <summary>
        /// Private key of the owner of the contracts that will be deployed
        /// </summary>
        public string ContractDeploymentOwnerPrivateKey { get; set; }

    }
}
