namespace Nethereum.Commerce.Contracts.Deployment.CompleteSample
{
    /// <summary>
    /// Config for deploying a complete sample set of contracts
    /// </summary>
    public class CompleteSampleDeploymentConfig
    {
        public EshopDeploymentCompleteSampleConfig EshopDeploymentConfig { get; set; }
        public SellerDeploymentCompleteSampleConfig SellerDeploymentConfig { get; set; }
        public SellerDeploymentCompleteSampleConfig SellerDeploymentConfig02 { get; set; }
    }
}
