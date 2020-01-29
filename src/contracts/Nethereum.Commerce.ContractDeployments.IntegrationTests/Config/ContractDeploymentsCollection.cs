using Xunit;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    [CollectionDefinition("Contract Deployment Collection")]
    public class ContractDeploymentsCollection : ICollectionFixture<ContractDeploymentsFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
