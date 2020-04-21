using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;
using static Nethereum.Commerce.Contracts.PurchasingExtensions;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Trait("Global", "")]
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
        public async void ShouldStoreRetrieveAndChangeSeller()
        {
            var sellerAdminContractAddress = _contracts.Deployment.SellerAdminService.ContractHandler.ContractAddress;

            // Create a Seller to store
            var sellerExpected = new Seller()
            {
                SellerId = "SellerToTest" + GetRandomString(),
                SellerDescription = "SellerDescription",
                AdminContractAddress = sellerAdminContractAddress,
                IsActive = true,
                CreatedByAddress = string.Empty // filled by contract
            };

            // Store Seller
            var txReceiptCreate = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Check Seller create events
            var logSellerCreateEvent = txReceiptCreate.DecodeAllEvents<SellerCreatedLogEventDTO>().FirstOrDefault();
            logSellerCreateEvent.Should().NotBeNull();
            logSellerCreateEvent.Event.SellerId.ConvertToString().Should().Be(sellerExpected.SellerId);

            // Retrieve Seller
            var sellerActual = (await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.GetSellerQueryAsync(sellerExpected.SellerId)).Seller;

            // They should be the same
            CheckEverySellerFieldMatches(sellerExpected, sellerActual, createdByAddress: _contracts.Web3.TransactionManager.Account.Address);

            // Change Seller
            sellerExpected.SellerDescription = "New description";
            var txReceiptChange = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            txReceiptChange.Status.Value.Should().Be(1);

            // Check Seller change events
            var logSellerChangeEvent = txReceiptChange.DecodeAllEvents<SellerChangedLogEventDTO>().FirstOrDefault();
            logSellerChangeEvent.Should().NotBeNull();
            logSellerChangeEvent.Event.SellerId.ConvertToString().Should().BeEquivalentTo(sellerExpected.SellerId);

            // Retrieve the updated Seller
            var sellerActualPostChange = (await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.GetSellerQueryAsync(sellerExpected.SellerId)).Seller;

            // Check seller values match
            CheckEverySellerFieldMatches(sellerExpected, sellerActualPostChange, createdByAddress: _contracts.Web3.TransactionManager.Account.Address);
        }

        [Fact]
        public async void ShouldStoreRetrieveAndChangeEshop()
        {
            // Create an eShop to store
            var eShopExpected = new Eshop()
            {
                EShopId = "eShopToTest" + GetRandomString(),
                EShopDescription = "eShopDescription",
                PurchasingContractAddress = "0x94618601FE6cb8912b274E5a00453949A57f8C1e",
                IsActive = true,
                CreatedByAddress = string.Empty,  // filled by contract
                QuoteSignerCount = 2,
                QuoteSigners = new List<string>()
                {
                    "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                    "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                }
            };

            // Store eShop
            var txReceiptCreate = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetEshopRequestAndWaitForReceiptAsync(eShopExpected);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Check eShop create events
            var logEshopCreateEvent = txReceiptCreate.DecodeAllEvents<EshopCreatedLogEventDTO>().FirstOrDefault();
            logEshopCreateEvent.Should().NotBeNull();
            logEshopCreateEvent.Event.EShopId.ConvertToString().Should().Be(eShopExpected.EShopId);

            // Retrieve eShop
            var eShopActual = (await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.GetEshopQueryAsync(eShopExpected.EShopId)).EShop;

            // They should be the same
            CheckEveryEshopFieldMatches(eShopExpected, eShopActual, createdByAddress: _contracts.Web3.TransactionManager.Account.Address);

            // Change eShop
            eShopExpected.EShopDescription = "New description";
            var txReceiptChange = await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetEshopRequestAndWaitForReceiptAsync(eShopExpected);
            txReceiptChange.Status.Value.Should().Be(1);

            // Check eShop change events
            var logEshopChangeEvent = txReceiptCreate.DecodeAllEvents<EshopCreatedLogEventDTO>().FirstOrDefault();
            logEshopChangeEvent.Should().NotBeNull();
            logEshopChangeEvent.Event.EShopId.ConvertToString().Should().BeEquivalentTo(eShopExpected.EShopId);

            // Retrieve the updated eShop
            var eShopActualPostChange = (await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.GetEshopQueryAsync(eShopExpected.EShopId)).EShop;

            // Check eshop values match
            CheckEveryEshopFieldMatches(eShopExpected, eShopActualPostChange, createdByAddress: _contracts.Web3.TransactionManager.Account.Address);
        }

        [Fact]
        public async void ShouldFailToCreateEshopWhenMissingPurchasingAddress()
        {
            // Create an eShop to store, but miss out required field PurchasingContractAddress            
            var eShopExpected = new Eshop()
            {
                EShopId = "eShopToTest" + GetRandomString(),
                EShopDescription = "eShopDescription",
                PurchasingContractAddress = string.Empty,  // causes error
                IsActive = true,
                CreatedByAddress = string.Empty,           // filled by contract
                QuoteSignerCount = 0,                      // filled by contract
                QuoteSigners = new List<string>()
                {
                    "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                    "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                }
            };

            // Try to store eShop, it should fail
            Func<Task> act = async () => await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetEshopRequestAndWaitForReceiptAsync(eShopExpected);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(BP_EXCEPTION_ESHOP_MISSING_PURCH_CONTRACT);
        }

        [Fact]
        public async void ShouldFailToCreateEshopWhenMissingSigners()
        {
            // Create an eShop to store, but miss out all quote signers            
            var eShopExpected = new Eshop()
            {
                EShopId = "eShopToTest" + GetRandomString(),
                EShopDescription = "eShopDescription",
                PurchasingContractAddress = "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                IsActive = true,
                CreatedByAddress = string.Empty,       // filled by contract
                QuoteSignerCount = 0,                  // filled by contract
                QuoteSigners = new List<string>() { }  // causes error
            };

            // Try to store eShop, it should fail
            Func<Task> act = async () => await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetEshopRequestAndWaitForReceiptAsync(eShopExpected);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(BP_EXCEPTION_ESHOP_MISSING_SIGNERS);
        }

        [Fact]
        public async void ShouldFailToCreateSellerWhenMissingAdminAddress()
        {
            // Create a Seller to store, but miss out required field adminContractAddress            
            var sellerExpected = new Seller()
            {
                SellerId = "Seller" + GetRandomString(),
                SellerDescription = "SellerDescription",
                AdminContractAddress = string.Empty,  // causes error
                IsActive = true,
                CreatedByAddress = string.Empty       // filled by contract
            };

            // Try to store Seller, it should fail
            Func<Task> act = async () => await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(BP_EXCEPTION_SELLER_MISSING_CONTRACT);
        }
    }
}
