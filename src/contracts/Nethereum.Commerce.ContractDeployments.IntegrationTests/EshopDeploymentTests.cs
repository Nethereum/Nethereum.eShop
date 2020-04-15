using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.Deployment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;
using static Nethereum.Commerce.Contracts.PurchasingExtensions;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Trait("Shop", "")]
    [Trait("Deployment", "")]
    [Collection("Contract Deployment Collection v2")]
    public class EshopDeploymentTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixturev2 _contracts;
        private readonly TestOutputHelperLogger _xunitlogger;

        public EshopDeploymentTests(ContractDeploymentsFixturev2 fixture, ITestOutputHelper output)
        {
            // See Output window -> Tests for fixture deployment logs.
            _contracts = fixture;
            _output = output;
            _xunitlogger = new TestOutputHelperLogger(_output);
        }

        [Fact]
        public async void ShouldDeployNewEshop()
        {
            var expectedEshopId = "Charlie" + GetRandomString();
            var expectedEshopDescription = expectedEshopId + " Description";

            var eshopDeployment = EshopDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 quoteSigners: new List<string>()
                 {
                     "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                     "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                 },
                 expectedEshopId,
                 expectedEshopDescription,
                 _xunitlogger);
            Func<Task> act = async () => await eshopDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();

            // If Eshop deployed ok then...
            // ...its global business partner storage address should have a value
            var bpStorageAddress = await eshopDeployment.PurchasingService.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            bpStorageAddress.IsValidNonZeroAddress().Should().BeTrue();

            // ...its global business partner service should be setup
            eshopDeployment.BusinessPartnerStorageGlobalService.Should().NotBeNull();

            // ...its eshopId should match expected
            eshopDeployment.EshopId.Should().Be(expectedEshopId);

            // ...there should be a global master data record for the seller
            BusinessPartnerStorageService bpss = eshopDeployment.BusinessPartnerStorageGlobalService;
            Eshop eshop = null;
            Func<Task> act2 = async () => eshop = (await bpss.GetEshopQueryAsync(expectedEshopId)).EShop;
            await act2.Should().NotThrowAsync();
            eshop.EShopId.Should().Be(expectedEshopId);
            eshop.EShopDescription.Should().Be(expectedEshopDescription);
        }

        [Fact]
        public async void ShouldFailToDeployNewEshopIfEshopIdAlreadyUsedByAnotherAddress()
        {
            // Create Eshop deployment as normal, which should be fine
            var expectedEshopId = "Charlie" + GetRandomString();
            var expectedEshopDescription = expectedEshopId + " Description";

            var eshopDeployment = EshopDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 quoteSigners: new List<string>()
                 {
                     "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                     "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                 },
                 expectedEshopId,
                 expectedEshopDescription,
                 _xunitlogger);
            Func<Task> act = async () => await eshopDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();

            // Using the secondary user, try to deploy ANOTHER Eshop, using the same EshopId.
            // This should fail, because the secondary user doesn't have permissions to overwrite 
            // the EshopId in the master data 
            var eshopDeployment2 = EshopDeployment.CreateFromNewDeployment(
                 _contracts.Web3SecondaryUser,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 quoteSigners: new List<string>()
                 {
                     "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                     "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                 },
                 expectedEshopId,
                 expectedEshopDescription,
                 _xunitlogger);
            Func<Task> act2 = async () => await eshopDeployment2.InitializeAsync();
            await act2.Should().ThrowAsync<SmartContractRevertException>().WithMessage("*Only createdByAddress can change this record*");
        }

        //[Fact]
        //public async void ShouldFailToDeployNewContractUsingBadBusinessPartnerAddress()
        //{
        //    var expectedSellerId = "Alice" + GetRandomString();
        //    var sellerDeployment = SellerDeployment.CreateFromNewDeployment(
        //         _contracts.Web3,
        //         "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",    // there is no global business partner storage here
        //         expectedSellerId, $"{expectedSellerId} Description",
        //         _xunitlogger);
        //    Func<Task> act = async () => await sellerDeployment.InitializeAsync();
        //    await act.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Could not create global business partner data for seller*");
        //}

        //[Fact]
        //public void ShouldFailToDeployNewContractWhenMissingWeb3()
        //{
        //    var expectedSellerId = "Alice" + GetRandomString();
        //    Action act = () => SellerDeployment.CreateFromNewDeployment(
        //         null,
        //         _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
        //         expectedSellerId, $"{expectedSellerId} Description",
        //         _xunitlogger);
        //    act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        //}

        [Fact]
        public async void ShouldConnectExistingEshop()
        {
            // Deploy an Eshop as normal
            var expectedEshopId = "Charlie" + GetRandomString();
            var expectedEshopDescription = expectedEshopId + " Description";

            var eshopDeployment = EshopDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 quoteSigners: new List<string>()
                 {
                     "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                     "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                 },
                 expectedEshopId,
                 expectedEshopDescription,
                 _xunitlogger);
            Func<Task> act = async () => await eshopDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();

            // Create an additional Eshop deployment by connecting to the existing first one
            var eshopDeployment2 = EshopDeployment.CreateFromConnectExistingContract(
                 _contracts.Web3,
                 eshopDeployment.PurchasingService.ContractHandler.ContractAddress,
                 _xunitlogger);
            Func<Task> act2 = async () => await eshopDeployment2.InitializeAsync();
            await act2.Should().NotThrowAsync();

            eshopDeployment2.Owner.Should().Be(eshopDeployment.Owner);
        }

        //[Fact]
        //public async void ShouldFailToConnectExistingWhenBadSellerContract()
        //{
        //    // Give a rubbish address for the existing seller wallet deployment
        //    var sellerDeployment = SellerDeployment.CreateFromConnectExistingContract(
        //         _contracts.Web3,
        //         "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9", // no seller contract deployed here
        //         _xunitlogger);
        //    Func<Task> act = async () => await sellerDeployment.InitializeAsync();
        //    await act.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Failed to set up*");
        //}

        //[Theory]
        //[InlineData(null)]
        //[InlineData("")]
        //[InlineData("a garbage address format")]
        //public void ShouldFailToConnectExistingWhenMissingSellerContractAddress(string sellerContractAddress)
        //{
        //    // Give some missing addresses for the existing seller wallet deployment
        //    Action act = () => SellerDeployment.CreateFromConnectExistingContract(
        //         _contracts.Web3,
        //         sellerContractAddress,
        //         _xunitlogger);
        //    act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        //}

        //[Theory]
        //[InlineData(null)]
        //[InlineData("")]
        //[InlineData("a garbage address format")]
        //public void ShouldFailToDeployNewContractWhenMissingBusinessPartnerAddress(string businessPartnerContractAddress)
        //{
        //    // Give some missing addresses for the global business partner contract address
        //    var expectedSellerId = "Alice" + GetRandomString();
        //    Action act = () => SellerDeployment.CreateFromNewDeployment(
        //         _contracts.Web3,
        //         businessPartnerContractAddress,
        //         expectedSellerId, $"{expectedSellerId} Description",
        //         _xunitlogger);
        //    act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        //}

        //[Fact]
        //public async void ShouldFailToConnectExistingWhenMissingWeb3()
        //{
        //    // Deploy a seller admin as normal
        //    var expectedSellerId = "Alice" + GetRandomString();
        //    var expectedSellerDescription = expectedSellerId + " Description";
        //    var sellerDeployment1 = SellerDeployment.CreateFromNewDeployment(
        //         _contracts.Web3,
        //         _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
        //         expectedSellerId,
        //         expectedSellerDescription,
        //         _xunitlogger);
        //    Func<Task> act1 = async () => await sellerDeployment1.InitializeAsync();
        //    await act1.Should().NotThrowAsync();

        //    // Create an additional seller admin deployment by connecting to the existing first one, which
        //    // will fail because no web3 supplied
        //    Action act2 = () => SellerDeployment.CreateFromConnectExistingContract(
        //        null,
        //        sellerDeployment1.SellerAdminService.ContractHandler.ContractAddress,
        //        _xunitlogger);
        //    act2.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        //}
    }
}
