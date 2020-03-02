namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Config to connect to an existing set of eShop contracts
    /// </summary>
    public class ContractConnectExistingConfig
    {
        /// <summary>
        /// eg , http://localhost:8545/, https://rinkeby.infura.io/v3/<key>
        /// </summary>
        public string BlockchainUrl { get; set; }

        public string WalletSellerAddress { get; set; }

        public string WalletBuyerAddress { get; set; }

        /// <summary>
        /// Private key for creating new transaactions during testing
        /// </summary>
        public string TransactionCreatorPrivateKey { get; set; }

    }
}
