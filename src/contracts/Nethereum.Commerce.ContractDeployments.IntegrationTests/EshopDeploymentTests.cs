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

        [Theory]
        [InlineData(new object[] { new string[] { } })]
        [InlineData(new object[] { new string[] { "", "" } })]
        [InlineData(new object[] { new string[] { "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9", "some garbage format" } })]
        public void ShouldFailToDeployNewEshopIfQuoteSignersMissing(string[] quoteSigners)
        {
            // Deploy an Eshop which should throw as quote signers are not valid
            var expectedEshopId = "Charlie" + GetRandomString();
            var expectedEshopDescription = expectedEshopId + " Description";
            Action act = () => EshopDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 quoteSigners: new List<string>(quoteSigners),
                 expectedEshopId,
                 expectedEshopDescription,
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up EshopDeployment*");
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

        [Fact]
        public async void ShouldFailToDeployNewEshopUsingBadBusinessPartnerAddress()
        {
            // Deploy an Eshop which should throw as global business partner storage address is not valid
            var expectedEshopId = "Charlie" + GetRandomString();
            var expectedEshopDescription = expectedEshopId + " Description";
            var eshopDeployment = EshopDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",    // there is no global business partner storage here
                 quoteSigners: new List<string>()
                 {
                     "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                     "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                 },
                 expectedEshopId,
                 expectedEshopDescription,
                 _xunitlogger);
            Func<Task> act = async () => await eshopDeployment.InitializeAsync();
            await act.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Fault with global business partner storage contract*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a garbage address format")]
        public void ShouldFailToDeployNewEshopWhenMissingBusinessPartnerAddress(string businessPartnerContractAddress)
        {
            // Give some missing addresses for the global business partner contract address
            var expectedEshopId = "Charlie" + GetRandomString();
            var expectedEshopDescription = expectedEshopId + " Description";
            Action act = () => EshopDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 businessPartnerContractAddress,
                 quoteSigners: new List<string>()
                 {
                     "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                     "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                 },
                 expectedEshopId,
                 expectedEshopDescription,
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public void ShouldFailToDeployNewEshopWhenMissingWeb3()
        {
            var expectedEshopId = "Charlie" + GetRandomString();
            var expectedEshopDescription = expectedEshopId + " Description";
            Action act = () => EshopDeployment.CreateFromNewDeployment(
                 null,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 quoteSigners: new List<string>()
                 {
                     "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",
                     "0x94618601FE6cb8912b274E5a00453949A57f8C1e"
                 },
                 expectedEshopId,
                 expectedEshopDescription,
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

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

        [Fact]
        public async void ShouldFailToConnectExistingWhenBadPurchasingContract()
        {
            // Give a rubbish address for the existing eshop deployment purchasing contract
            var eshopDeployment = EshopDeployment.CreateFromConnectExistingContract(
                 _contracts.Web3,
                 "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9", // no purchasing contract deployed here
                 _xunitlogger);
            Func<Task> act = async () => await eshopDeployment.InitializeAsync();
            await act.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a garbage address format")]
        public void ShouldFailToConnectExistingWhenMissingPurchasingContractAddress(string purchasingContractAddress)
        {
            // Give some missing addresses for the existing purchasing contract deployment
            Action act = () => EshopDeployment.CreateFromConnectExistingContract(
                           _contracts.Web3,
                           purchasingContractAddress,
                           _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldFailToConnectExistingWhenMissingWeb3()
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

            // Create an additional eshop deployment by connecting to the existing first one, which
            // will fail because no web3 supplied
            Action act2 = () => EshopDeployment.CreateFromConnectExistingContract(
                null,
                eshopDeployment.PurchasingService.ContractHandler.ContractAddress,
                _xunitlogger);
            act2.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }
    }
}
