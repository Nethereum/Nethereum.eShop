using System.Collections.Generic;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Config for deploying a complete sample set of contracts
    /// </summary>
    public class CompleteSampleDeploymentConfig
    {
        EshopDeploymentConfig EshopDeploymentConfig { get; set; }
        BuyerDeploymentConfig BuyerDeploymentConfig { get; set; }
        BuyerDeploymentConfig BuyerDeploymentConfig02 { get; set; }
        SellerDeploymentConfig SellerDeploymentConfig { get; set; }
        SellerDeploymentConfig SellerDeploymentConfig02 { get; set; }
    }
}
