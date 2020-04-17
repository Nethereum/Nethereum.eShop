using System.Collections.Generic;

namespace Nethereum.Commerce.Contracts.Deployment.CompleteSample
{
    /// <summary>
    /// Config for deploying a new set of eShop contracts as part of a complete deployment
    /// </summary>
    public class EshopDeploymentCompleteSampleConfig
    {
        public string EshopId { get; set; }
        public string EshopDescription { get; set; }
        public List<string> QuoteSigners { get; set; }
    }
}
