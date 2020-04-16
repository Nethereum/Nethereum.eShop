using System.Collections.Generic;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Config for deploying a new set of eShop contracts
    /// </summary>
    public class EshopDeploymentConfig
    {
        string BusinessPartnerStorageGlobalAddress { get; set; }
        string EshopId { get; set; }
        string EshopDescription { get; set; }
        List<string> QuoteSigners { get; set; }
    }
}
