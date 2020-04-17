using Xunit;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests.Config
{
    [CollectionDefinition("GlobalBusinessPartnersFixture")]
    public class GlobalBusinessPartnersFixtureCollection : ICollectionFixture<GlobalBusinessPartnersFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
