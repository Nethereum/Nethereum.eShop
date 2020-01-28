using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class DeploymentTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contractDeployments;

        public DeploymentTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contractDeployments = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldHaveDeployedAndConfiguredAllContracts()
        {
            // If all contracts deployed and configured ok, then the PO storage contract
            // should be configured to point to the eternal storage contract.
            var actualContractAddress = await _contractDeployments.PoStorageService.EternalStorageQueryAsync();
            var expectedContractAddress = _contractDeployments.EternalStorageService.ContractHandler.ContractAddress;
            actualContractAddress.Should().Be(expectedContractAddress);
        }
    }
}
