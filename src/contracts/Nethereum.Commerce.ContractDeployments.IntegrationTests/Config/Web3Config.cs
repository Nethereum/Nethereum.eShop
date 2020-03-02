namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    /// <summary>
    /// Config for Web3 object
    /// </summary>
    public class Web3Config
    {
        /// <summary>
        /// eg , http://localhost:8545/, https://rinkeby.infura.io/v3/<key>
        /// </summary>
        public string BlockchainUrl { get; set; }

        /// <summary>
        /// Private key for posting new transaactions
        /// </summary>
        public string TransactionCreatorPrivateKey { get; set; }
    }
}
