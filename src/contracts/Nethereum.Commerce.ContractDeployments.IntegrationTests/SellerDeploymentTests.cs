using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.Deployment;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Trait("Seller", "")]
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
            var expectedSellerId = "Alice";
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
            bpStorageAddress.Should().NotBeNullOrEmpty();
            bpStorageAddress.IsZeroAddress().Should().BeFalse();

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
        public async void ShouldFailToDeployNewContractUsingBadBusinessPartnerAddress()
        {
            var expectedSellerId = "Alice";
            var sellerDeployment = SellerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9",    // <- there is no global business partner storage here
                 expectedSellerId, $"{expectedSellerId} Description",
                 _xunitlogger);
            Func<Task> act = async () => await sellerDeployment.InitializeAsync();
            await act.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Could not create global business partner data for seller*");
        }

        [Fact]
        public async void ShouldConnectExistingContract()
        {
            // Deploy a buyer wallet
            var buyerDeployment = BuyerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 _xunitlogger);
            Func<Task> act = async () => await buyerDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();

            // Create an additional buyer wallet by connecting to the existing first one
            var buyerDeployment2 = BuyerDeployment.CreateFromConnectExistingContract(
                _contracts.Web3,
                buyerDeployment.BuyerWalletService.ContractHandler.ContractAddress,
                _xunitlogger);
            Func<Task> act2 = async () => await buyerDeployment2.InitializeAsync();
            await act2.Should().NotThrowAsync();

            buyerDeployment2.Owner.Should().Be(buyerDeployment.Owner);
        }

        [Fact]
        public async void ShouldFailToConnectNonExistingContract()
        {
            // Give a rubbish address for the existing buyer wallet deployment
            var buyerDeployment = BuyerDeployment.CreateFromConnectExistingContract(
                 _contracts.Web3,
                 "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9", // no buyer contract deployed here
                 _xunitlogger);
            Func<Task> act = async () => await buyerDeployment.InitializeAsync();
            await act.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }
    }
}
