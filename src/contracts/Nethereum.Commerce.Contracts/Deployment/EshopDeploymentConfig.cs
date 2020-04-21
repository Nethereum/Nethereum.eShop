using System.Collections.Generic;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Config for deploying a new set of eShop contracts
    /// </summary>
    public class EshopDeploymentConfig
    {
        public string BusinessPartnerStorageGlobalAddress { get; set; }
        public string EshopId { get; set; }
        public string EshopDescription { get; set; }
        public List<string> QuoteSigners { get; set; }
    }
}
