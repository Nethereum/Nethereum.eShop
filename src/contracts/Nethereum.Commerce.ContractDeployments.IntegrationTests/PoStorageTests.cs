using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using System.Collections.Generic;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class PoStorageTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public PoStorageTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldStoreAndRetrievePo()
        {
            // Create a PO to store
            Po po = new Po()
            {
                BuyerAddress = "0x",

            };
            po.PoItems = new List<PoItem>();

            // Store it
            var txReceipt = await _contracts.PoStorageService.SetPoRequestAndWaitForReceiptAsync(po);
            //var expectedContractAddress = _contracts.EternalStorageService.ContractHandler.ContractAddress;
            
            // Retrieve it

            // It should be the same
            //actualContractAddress.Should().Be(expectedContractAddress);
        }
    }
}
