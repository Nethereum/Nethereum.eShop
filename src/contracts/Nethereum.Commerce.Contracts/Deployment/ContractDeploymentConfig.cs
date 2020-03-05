namespace Nethereum.Commerce.Contracts.Deployment
{
    /// <summary>
    /// Config for deploying a new set of eShop contracts
    /// </summary>
    public class ContractDeploymentConfig
    {      
        /// <summary>
        /// eShop seller id, 32 chars max, eg "Nethereum.eShop"
        /// </summary>
        public string EShopSellerId { get; set; }

        /// <summary>
        /// eShop description, eg "Satoshi's Books"
        /// </summary>
        public string EShopDescription { get; set; }

        /// <summary>
        /// EoA or contract address, the signer who can a sign a quotation tx to prove shop approves it
        /// </summary>
        public string EShopApproverAddress { get; set; }
        
        /// <summary>
        /// Also deploy mock contracts eg for DAI
        /// </summary>
        public bool AlsoDeployMockContracts { get; set; }
    }
}
