using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.Contracts.ContractEnums;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class BusinessPartnerStorageTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public BusinessPartnerStorageTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldStoreAndRetrieveSeller()
        {
            // Create a Seller to store
            var sellerExpected = new Seller()
            {
                SellerId = "SellerToTest",
                SellerDescription = _contracts.ContractDeploymentConfig.EShopDescription,
                FinanceAddress = _contracts.ContractDeploymentConfig.EShopFinanceAddress,
                ApproverAddress = _contracts.ContractDeploymentConfig.EShopApproverAddress,
                IsActive = true
            };

            // Store Seller
            var txReceipt = await _contracts.BusinessPartnerStorageService.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve Seller
            var sellerActual = (await _contracts.BusinessPartnerStorageService.GetSellerQueryAsync(sellerExpected.SellerId)).Seller;

            // They should be the same
            CheckEverySellerFieldMatches(sellerExpected, sellerActual);
        }

        private static void CheckEverySellerFieldMatches(Seller sellerExpected, Seller sellerActual)
        {
            sellerActual.SellerId.Should().Be(sellerExpected.SellerId);
            sellerActual.SellerDescription.Should().Be(sellerExpected.SellerDescription);
            sellerActual.FinanceAddress.Should().Be(sellerExpected.FinanceAddress);
            sellerActual.ApproverAddress.Should().Be(sellerExpected.ApproverAddress);
            sellerActual.IsActive.Should().Be(sellerExpected.IsActive);
        }
    }
}
