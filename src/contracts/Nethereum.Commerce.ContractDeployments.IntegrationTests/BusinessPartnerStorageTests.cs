using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Xunit;
using Xunit.Abstractions;

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
            var sellerAdminContractAddress = _contracts.Deployment.WalletSellerService.ContractHandler.ContractAddress;

            // Create a Seller to store
            var sellerExpected = new Seller()
            {
                SellerId = "SellerToTest",
                SellerDescription = "SellerDescription",
                AdminContractAddress = sellerAdminContractAddress,
                IsActive = true,
                CreatedByAddress = string.Empty // filled by contract
            };

            // Store Seller
            var txReceipt = await _contracts.Deployment.BusinessPartnerStorageService.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve Seller
            var sellerActual = (await _contracts.Deployment.BusinessPartnerStorageService.GetSellerQueryAsync(sellerExpected.SellerId)).Seller;

            // They should be the same
            CheckEverySellerFieldMatches(sellerExpected, sellerActual, createdByAddress: _contracts.Web3.TransactionManager.Account.Address);
        }

        [Fact]
        public async void ShouldStoreAndRetrieveEshop()
        {
            // Create an eShop to store
            var eShopExpected = new Eshop()
            {
                EShopId = "eShopToTest",
                EShopDescription = "eShopDescription",
                PurchasingContractAddress = "0x94618601FE6cb8912b274E5a00453949A57f8C1e",
                QuoteSignerAddress = "0x94618601FE6cb8912b274E5a00453949A57f8C1e",
                IsActive = true,
                CreatedByAddress = string.Empty // filled by contract
            };

            // Store eShop
            var txReceipt = await _contracts.Deployment.BusinessPartnerStorageService.SetEshopRequestAndWaitForReceiptAsync(eShopExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve eShop
            var eShopActual = (await _contracts.Deployment.BusinessPartnerStorageService.GetEshopQueryAsync(eShopExpected.EShopId)).EShop;

            // They should be the same
            CheckEveryEshopFieldMatches(eShopExpected, eShopActual, createdByAddress: _contracts.Web3.TransactionManager.Account.Address);
        }

        private static void CheckEverySellerFieldMatches(Seller sellerExpected, Seller sellerActual, string createdByAddress)
        {
            sellerActual.SellerId.Should().Be(sellerExpected.SellerId);
            sellerActual.SellerDescription.Should().Be(sellerExpected.SellerDescription);
            sellerActual.AdminContractAddress.ToLowerInvariant().Should().Be(sellerExpected.AdminContractAddress.ToLowerInvariant());
            sellerActual.IsActive.Should().Be(sellerExpected.IsActive);
            sellerActual.CreatedByAddress.ToLowerInvariant().Should().Be(createdByAddress.ToLowerInvariant());
        }

        private static void CheckEveryEshopFieldMatches(Eshop eShopExpected, Eshop eShopActual, string createdByAddress)
        {
            eShopActual.EShopId.Should().Be(eShopExpected.EShopId);
            eShopActual.EShopDescription.Should().Be(eShopExpected.EShopDescription);
            eShopActual.PurchasingContractAddress.ToLowerInvariant().Should().Be(eShopExpected.PurchasingContractAddress.ToLowerInvariant());
            eShopActual.QuoteSignerAddress.ToLowerInvariant().Should().Be(eShopExpected.QuoteSignerAddress.ToLowerInvariant());
            eShopActual.IsActive.Should().Be(eShopExpected.IsActive);
            eShopActual.CreatedByAddress.ToLowerInvariant().Should().Be(createdByAddress.ToLowerInvariant());
        }
    }
}
