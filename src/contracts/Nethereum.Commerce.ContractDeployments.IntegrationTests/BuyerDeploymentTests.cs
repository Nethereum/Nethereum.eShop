using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.Deployment;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Nethereum.Commerce.Contracts;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Trait("Buyer", "")]
    [Trait("Deployment", "")]
    [Collection("GlobalBusinessPartnersFixture")]
    public class BuyerDeploymentTests
    {
        private readonly ITestOutputHelper _output;
        private readonly GlobalBusinessPartnersFixture _fixtureContracts;
        private readonly TestOutputHelperLogger _xunitlogger;

        public BuyerDeploymentTests(GlobalBusinessPartnersFixture fixture, ITestOutputHelper output)
        {
            // See Output window -> Tests for fixture deployment logs.
            _fixtureContracts = fixture;
            _output = output;
            _xunitlogger = new TestOutputHelperLogger(_output);
        }

        [Fact]
        public async void ShouldDeployNewContract()
        {
            var buyerDeployment = BuyerDeployment.CreateFromNewDeployment(
                 _fixtureContracts.Web3,
                 new BuyerDeploymentConfig() { BusinessPartnerStorageGlobalAddress = _fixtureContracts.BusinessPartnersContractAddress },
                 _xunitlogger);
            Func<Task> act = async () => await buyerDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();

            // If buyer deployed ok then...
            // ...its global business partner storage address should have a value
            var bpStorageAddress = await buyerDeployment.BuyerWalletService.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            bpStorageAddress.IsValidNonZeroAddress().Should().BeTrue();
            
            // ...its global business partner service should be setup
            buyerDeployment.BusinessPartnerStorageGlobalService.Should().NotBeNull();
        }

        [Fact]
        public void ShouldFailToDeployNewContractWhenMissingWeb3()
        {
            Action act = () => BuyerDeployment.CreateFromNewDeployment(
                 null,
                 new BuyerDeploymentConfig() { BusinessPartnerStorageGlobalAddress = _fixtureContracts.BusinessPartnersContractAddress },
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a garbage address format")]
        public void ShouldFailToDeployNewContractWhenMissingBusinessPartnerAddress(string businessPartnerContractAddress)
        {
            // Give some missing addresses for the existing buyer wallet deployment
            Action act = () => BuyerDeployment.CreateFromNewDeployment(
                 _fixtureContracts.Web3,
                 new BuyerDeploymentConfig() { BusinessPartnerStorageGlobalAddress = businessPartnerContractAddress },
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldFailToDeployNewContractWhenBadBusinessPartnerAddress()
        {
            // Give a technically valid addess, but not an address for a valid business partner storage contract
            var buyerDeployment1 = BuyerDeployment.CreateFromNewDeployment(
                 _fixtureContracts.Web3,
                 new BuyerDeploymentConfig() { BusinessPartnerStorageGlobalAddress = "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9" }, // no business partner storage contract deployed here
                 _xunitlogger);
            Func<Task> act1 = async () => await buyerDeployment1.InitializeAsync();
            await act1.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldConnectExistingContract()
        {
            // Deploy a buyer wallet
            var buyerDeployment1 = BuyerDeployment.CreateFromNewDeployment(
                 _fixtureContracts.Web3,
                 new BuyerDeploymentConfig() { BusinessPartnerStorageGlobalAddress = _fixtureContracts.BusinessPartnersContractAddress },
                 _xunitlogger);
            Func<Task> act = async () => await buyerDeployment1.InitializeAsync();
            await act.Should().NotThrowAsync();

            // Create an additional buyer wallet deployment by connecting to the existing first one
            var buyerDeployment2 = BuyerDeployment.CreateFromConnectExistingContract(
                _fixtureContracts.Web3,
                buyerDeployment1.BuyerWalletService.ContractHandler.ContractAddress,
                _xunitlogger);
            Func<Task> act2 = async () => await buyerDeployment2.InitializeAsync();
            await act2.Should().NotThrowAsync();

            buyerDeployment2.Owner.Should().Be(buyerDeployment1.Owner);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a garbage address format")]
        public void ShouldFailToConnectExistingWhenMissingBuyerContractAddress(string buyerContractAddress)
        {
            // Give some missing addresses for the existing buyer wallet deployment
            Action act = () => BuyerDeployment.CreateFromConnectExistingContract(
                 _fixtureContracts.Web3,
                 buyerContractAddress,
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldFailToConnectExistingWhenBadBuyerContractAddress()
        {
            // Give a technically valid addess, but not an address for an existing buyer wallet deployment
            var buyerDeployment = BuyerDeployment.CreateFromConnectExistingContract(
                 _fixtureContracts.Web3,
                 "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9", // no buyer contract deployed here
                 _xunitlogger);
            Func<Task> act = async () => await buyerDeployment.InitializeAsync();
            await act.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldFailToConnectExistingWhenMissingWeb3()
        {
            // Deploy a buyer wallet as normal
            var buyerDeployment1 = BuyerDeployment.CreateFromNewDeployment(
                 _fixtureContracts.Web3,
                 new BuyerDeploymentConfig() { BusinessPartnerStorageGlobalAddress = _fixtureContracts.BusinessPartnersContractAddress },
                 _xunitlogger);
            Func<Task> act1 = async () => await buyerDeployment1.InitializeAsync();
            await act1.Should().NotThrowAsync();

            // Create an additional buyer wallet deployment by connecting to the existing first one, which
            // will fail because no web3 supplied
            Action act2 = () => BuyerDeployment.CreateFromConnectExistingContract(
                 null,
                 buyerDeployment1.BuyerWalletService.ContractHandler.ContractAddress,
                 _xunitlogger);
            act2.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }
    }
}
