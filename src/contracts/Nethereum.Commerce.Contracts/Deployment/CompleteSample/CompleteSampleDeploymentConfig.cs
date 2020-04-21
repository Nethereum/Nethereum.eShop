namespace Nethereum.Commerce.Contracts.Deployment.CompleteSample
{
    /// <summary>
    /// Config for deploying a complete sample set of contracts
    /// </summary>
    public class CompleteSampleDeploymentConfig
    {
        public EshopDeploymentCompleteSampleConfig Eshop { get; set; }
        public SellerDeploymentCompleteSampleConfig Seller { get; set; }
        public SellerDeploymentCompleteSampleConfig Seller02 { get; set; }
    }
}
