using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Nethereum.Commerce.Contracts;

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

            // ... the buyer wallet should be configured to point to the purchasing contract.
            var actualPurchasingAddressHeldAgainstBuyerWallet = await _contracts.WalletBuyerService.PurchasingQueryAsync();
            var expectedPurchasingAddress = _contracts.PurchasingService.ContractHandler.ContractAddress;
            actualPurchasingAddressHeldAgainstBuyerWallet.Should().Be(expectedPurchasingAddress);

            // ... the seller wallet should be configured to point to the purchasing contract.
            var actualPurchasingAddressHeldAgainstSellerWallet = await _contracts.WalletSellerService.PurchasingQueryAsync();
            actualPurchasingAddressHeldAgainstSellerWallet.Should().Be(expectedPurchasingAddress);

            // ... the seller wallet should be configured to have a seller id.
            var actualSellerIdString = ConversionUtils.ConvertBytes32ArrayToString(await _contracts.WalletSellerService.SellerIdQueryAsync());
            var expectedSellerIdString = _contracts.ContractDeploymentConfig.EShopSellerId;
            actualSellerIdString.Should().Be(expectedSellerIdString);

            // ... and that seller id should have a master data entry in business partner storage.
            var actualSellerIdBytes = ConversionUtils.ConvertStringToBytes32Array(actualSellerIdString);
            var actualSellerIdRecordFromBusinessPartnerStorage = (await _contracts.BusinessPartnerStorageService.GetSellerQueryAsync(actualSellerIdBytes)).Seller;
            actualSellerIdRecordFromBusinessPartnerStorage.IsActive.Should().Be(true);
            actualSellerIdRecordFromBusinessPartnerStorage.SellerDescription.Should().Be(_contracts.ContractDeploymentConfig.EShopDescription);

        }
    }
}
