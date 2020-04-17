namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Config for deploying a new Seller
    /// </summary>
    public class SellerDeploymentConfig
    {
        public string BusinessPartnerStorageGlobalAddress { get; set; }
        public string SellerId { get; set; }
        public string SellerDescription { get; set; }
    }
}
