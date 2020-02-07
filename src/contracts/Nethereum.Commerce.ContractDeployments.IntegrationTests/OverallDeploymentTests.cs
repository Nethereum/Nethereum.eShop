using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class OverallDeploymentTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public OverallDeploymentTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldHaveDeployedAndConfiguredAllContracts()
        {
            // If all contracts deployed and configured ok, then...
            // ...the PO storage contract should be configured to point to the eternal storage contract.
            var actualEternalStorageAddressHeldAgainstPoStorage = await _contracts.PoStorageService.EternalStorageQueryAsync();
            var expectedEternalStorageAddress = _contracts.EternalStorageService.ContractHandler.ContractAddress;
            actualEternalStorageAddressHeldAgainstPoStorage.Should().Be(expectedEternalStorageAddress);
            
            // ...the funding contract should be configured to point to the business partner storage contract.
            var actualBusinessPartnerStorageAddressHeldAgainstFunding = await _contracts.FundingService.BusinessPartnerStorageQueryAsync();
            var expectedBusinessPartnerAddress = _contracts.BusinessPartnerStorageService.ContractHandler.ContractAddress;
            actualBusinessPartnerStorageAddressHeldAgainstFunding.Should().Be(expectedBusinessPartnerAddress);
        }
    }
}
