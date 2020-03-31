using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;

namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Config for deploying a new set of eShop contracts
    /// </summary>
    public class ContractNewDeploymentConfig
    {     
        /// <summary>
        /// Definition of the eShop
        /// </summary>
        public Eshop Eshop { get; set; }

        /// <summary>
        /// eShop needs at least one seller
        /// </summary>
        public Seller Seller { get; set; }
        
        /// <summary>
        /// Also deploy mock contracts eg for DAI
        /// </summary>
        public bool AlsoDeployMockContracts { get; set; }
    }
}
