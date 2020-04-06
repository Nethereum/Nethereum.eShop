using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class BusinessPartnerAuthTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public BusinessPartnerAuthTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldChangeSellerWhenCreatedBy()
        {
            // Create a Seller to store
            var sellerAdminContractAddress = _contracts.Deployment.SellerAdminService.ContractHandler.ContractAddress;
            var sellerExpected = new Seller()
            {
                SellerId = "Seller" + GetRandomString(),
                SellerDescription = "SellerDescription",
                AdminContractAddress = sellerAdminContractAddress,
                IsActive = true,
                CreatedByAddress = string.Empty // filled by contract
            };

            // Store Seller
            var txReceipt = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Change Seller
            sellerExpected.SellerDescription = "New description";
            txReceipt = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve Seller
            var sellerActualPostChange = (await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.GetSellerQueryAsync(sellerExpected.SellerId)).Seller;

            // They should be the same
            CheckEverySellerFieldMatches(sellerExpected, sellerActualPostChange, createdByAddress: _contracts.Web3.TransactionManager.Account.Address);
        }

        [Fact]
        public async void ShouldChangeEshopWhenCreatedBy()
        {
            // Create an eShop to store
            var eShopExpected = new Eshop()
            {
                EShopId = "eShopToTest" + GetRandomString(),
                EShopDescription = "eShopDescription",
                PurchasingContractAddress = "0x94618601FE6cb8912b274E5a00453949A57f8C1e",
                IsActive = true,
                CreatedByAddress = string.Empty, // filled by contract
                QuoteSignerCount = 2,
                QuoteSigners = new List<string>()
                {
                    "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                    "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                }
            };

            // Store eShop
            var txReceipt = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetEshopRequestAndWaitForReceiptAsync(eShopExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Change eShop
            eShopExpected.EShopDescription = "New description";
            txReceipt = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetEshopRequestAndWaitForReceiptAsync(eShopExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve eShop
            var eShopActualPostChange = (await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.GetEshopQueryAsync(eShopExpected.EShopId)).EShop;

            // They should be the same
            CheckEveryEshopFieldMatches(eShopExpected, eShopActualPostChange, createdByAddress: _contracts.Web3.TransactionManager.Account.Address);
        }

        [Fact]
        public async void ShouldFailToChangeSellerWhenNotCreatedBy()
        {
            // Create a seller, then try to change it with another account, it should fail
            var sellerAdminContractAddress = _contracts.Deployment.SellerAdminService.ContractHandler.ContractAddress;
            var sellerExpected = new Seller()
            {
                SellerId = "Seller" + GetRandomString(),
                SellerDescription = "SellerDescription",
                AdminContractAddress = sellerAdminContractAddress,
                IsActive = true,
                CreatedByAddress = string.Empty // filled by contract
            };

            // Store Seller
            var txReceipt = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Try to change Seller using preexisting bp storage service contract, but with tx executed by a different ("secondary") user
            sellerExpected.SellerDescription = "New description";
            var bpss = new BusinessPartnerStorageService(_contracts.Web3SecondaryUser, _contracts.Deployment.BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress);
            Func<Task> act = async () => await bpss.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_CREATEDBY);
        }

        [Fact]
        public async void ShouldFailToChangeEshopWhenNotCreatedBy()
        {
            // Create an eShop, then try to change it with another account, it should fail
            var eShopExpected = new Eshop()
            {
                EShopId = "eShopToTest" + GetRandomString(),
                EShopDescription = "eShopDescription",
                PurchasingContractAddress = "0x94618601FE6cb8912b274E5a00453949A57f8C1e",
                IsActive = true,
                CreatedByAddress = string.Empty, // filled by contract
                QuoteSignerCount = 2,
                QuoteSigners = new List<string>()
                {
                    "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                    "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                }
            };

            // Store eShop
            var txReceipt = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetEshopRequestAndWaitForReceiptAsync(eShopExpected);
            txReceipt.Status.Value.Should().Be(1);

            // Try to change eShop using preexisting bp storage service contract, but with tx executed by a different ("secondary") user
            eShopExpected.EShopDescription = "New description";
            var bpss = new BusinessPartnerStorageService(_contracts.Web3SecondaryUser, _contracts.Deployment.BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress);
            Func<Task> act = async () => await bpss.SetEshopRequestAndWaitForReceiptAsync(eShopExpected);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_CREATEDBY);
        }
    }
}