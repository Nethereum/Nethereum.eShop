using Xunit;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    [CollectionDefinition("Contract Deployment Collection v2")]
    public class ContractDeploymentsCollectionv2 : ICollectionFixture<ContractDeploymentsFixturev2>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
