namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Config for deploying a new Seller
    /// </summary>
    public class SellerDeploymentConfig
    {
        string BusinessPartnerStorageGlobalAddress { get; set; }
        string SellerId { get; set; }
        string SellerDescription { get; set; }
    }
}
