namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Config to connect to an existing set of eShop contracts
    /// </summary>
    public class ContractConnectExistingConfig
    {
        public string WalletSellerAddress { get; set; }

        public string WalletBuyerAddress { get; set; }
    }
}
