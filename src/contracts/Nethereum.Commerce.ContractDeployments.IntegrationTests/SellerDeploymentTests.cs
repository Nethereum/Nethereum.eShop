using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using Nethereum.Commerce.Contracts.Deployment;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;
using static Nethereum.Commerce.Contracts.PurchasingExtensions;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Trait("Seller", "")]
    [Trait("Deployment", "")]
    [Collection("Contract Deployment Collection v2")]
    public class SellerDeploymentTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixturev2 _contracts;
        private readonly TestOutputHelperLogger _xunitlogger;

        public SellerDeploymentTests(ContractDeploymentsFixturev2 fixture, ITestOutputHelper output)
        {
            // See Output window -> Tests for fixture deployment logs.
            _contracts = fixture;
            _output = output;
            _xunitlogger = new TestOutputHelperLogger(_output);
        }

        [Fact]
        public async void ShouldDeployNewContract()
        {
            var expectedSellerId = "Alice" + GetRandomString();
            var expectedSellerDescription = expectedSellerId + " Description";
            var sellerDeployment = SellerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 expectedSellerId,
                 expectedSellerDescription,
                 _xunitlogger);
            Func<Task> act = async () => await sellerDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();

            // If seller deployed ok then...
            // ...its global business partner storage address should have a value
            var bpStorageAddress = await sellerDeployment.SellerAdminService.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            bpStorageAddress.IsValidNonZeroAddress().Should().BeTrue();

            // ...its sellerId should match expected
            sellerDeployment.SellerId.Should().Be(expectedSellerId);

            // ...there should be a global master data record for the seller
            BusinessPartnerStorageService bpss = new BusinessPartnerStorageService(_contracts.Web3, bpStorageAddress);
            Seller seller = null;
            Func<Task> act2 = async () => seller = (await bpss.GetSellerQueryAsync(expectedSellerId)).Seller;
            await act2.Should().NotThrowAsync();
            seller.SellerId.Should().Be(expectedSellerId);
            seller.SellerDescription.Should().Be(expectedSellerDescription);
        }

        [Fact]
        public async void ShouldFailToDeployNewContractIfSellerAlreadyUsedByAnotherAddress()
        {
            // Create Seller deployment as normal, which should be fine
            var expectedSellerId = "Alice" + GetRandomString();
            var sellerDeployment1 = SellerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 expectedSellerId, $"{expectedSellerId} Description",
                 _xunitlogger);
            Func<Task> act1 = async () => await sellerDeployment1.InitializeAsync();
            await act1.Should().NotThrowAsync();

            // Using the secondary user, try to deploy ANOTHER seller, using the same sellerId.
            // This should fail, because the secondary user doesn't have permissions to overwrite 
            // the sellerId in the master data 
            var sellerDeployment2 = SellerDeployment.CreateFromNewDeployment(
                 _contracts.Web3SecondaryUser,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 expectedSellerId, $"{expectedSellerId} Description",
                 _xunitlogger);
            Func<Task> act2 = async () => await sellerDeployment2.InitializeAsync();
            await act2.Should().ThrowAsync<SmartContractRevertException>().WithMessage("*Only createdByAddress can change this record*");
        }

        [Fact]
        public async void ShouldFailToDeployNewContractUsingBadBusinessPartnerAddress()
        {
            var expectedSellerId = "Alice" + GetRandomString();
            var sellerDeployment = SellerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",    // there is no global business partner storage here
                 expectedSellerId, $"{expectedSellerId} Description",
                 _xunitlogger);
            Func<Task> act = async () => await sellerDeployment.InitializeAsync();
            await act.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Could not create global business partner data for seller*");
        }

        [Fact]
        public void ShouldFailToDeployNewContractWhenMissingWeb3()
        {
            var expectedSellerId = "Alice" + GetRandomString();
            Action act = () => SellerDeployment.CreateFromNewDeployment(
                 null,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 expectedSellerId, $"{expectedSellerId} Description",
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldConnectExistingContract()
        {
            // Deploy a seller admin as normal
            var expectedSellerId = "Alice" + GetRandomString();
            var expectedSellerDescription = expectedSellerId + " Description";
            var sellerDeployment1 = SellerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 expectedSellerId,
                 expectedSellerDescription,
                 _xunitlogger);
            Func<Task> act1 = async () => await sellerDeployment1.InitializeAsync();
            await act1.Should().NotThrowAsync();

            // Create an additional seller admin deployment by connecting to the existing first one
            var sellerDeployment2 = SellerDeployment.CreateFromConnectExistingContract(
                _contracts.Web3,
                sellerDeployment1.SellerAdminService.ContractHandler.ContractAddress,
                _xunitlogger);
            Func<Task> act2 = async () => await sellerDeployment2.InitializeAsync();
            await act2.Should().NotThrowAsync();

            sellerDeployment2.Owner.Should().Be(sellerDeployment1.Owner);
        }

        [Fact]
        public async void ShouldFailToConnectExistingWhenBadSellerContract()
        {
            // Give a rubbish address for the existing seller wallet deployment
            var sellerDeployment = SellerDeployment.CreateFromConnectExistingContract(
                 _contracts.Web3,
                 "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9", // no seller contract deployed here
                 _xunitlogger);
            Func<Task> act = async () => await sellerDeployment.InitializeAsync();
            await act.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a garbage address format")]
        public void ShouldFailToConnectExistingWhenMissingSellerContractAddress(string sellerContractAddress)
        {
            // Give some missing addresses for the existing seller wallet deployment
            Action act = () => SellerDeployment.CreateFromConnectExistingContract(
                 _contracts.Web3,
                 sellerContractAddress,
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a garbage address format")]
        public void ShouldFailToDeployNewContractWhenMissingBusinessPartnerAddress(string businessPartnerContractAddress)
        {
            // Give some missing addresses for the global business partner contract address
            var expectedSellerId = "Alice" + GetRandomString();
            Action act = () => SellerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 businessPartnerContractAddress,
                 expectedSellerId, $"{expectedSellerId} Description",
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldFailToConnectExistingWhenMissingWeb3()
        {
            // Deploy a seller admin as normal
            var expectedSellerId = "Alice" + GetRandomString();
            var expectedSellerDescription = expectedSellerId + " Description";
            var sellerDeployment1 = SellerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 expectedSellerId,
                 expectedSellerDescription,
                 _xunitlogger);
            Func<Task> act1 = async () => await sellerDeployment1.InitializeAsync();
            await act1.Should().NotThrowAsync();

            // Create an additional seller admin deployment by connecting to the existing first one, which
            // will fail because no web3 supplied
            Action act2 = () => SellerDeployment.CreateFromConnectExistingContract(
                null,
                sellerDeployment1.SellerAdminService.ContractHandler.ContractAddress,
                _xunitlogger);
            act2.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }
    }
}
