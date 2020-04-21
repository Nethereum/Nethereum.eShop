namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    /// <summary>
    /// Config for secondary users, these are users without ownership rights 
    /// to the deployed contracts
    /// </summary>
    public class Web3SecondaryConfig
    {
        /// <summary>
        /// Private key for posting new transaactions
        /// </summary>
        public string UserPrivateKey { get; set; }
    }
}
