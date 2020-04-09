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
    [Collection("Contract Deployment Collection v2")]
    public class BuyerDeploymentTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixturev2 _contracts;
        private readonly TestOutputHelperLogger _xunitlogger;

        public BuyerDeploymentTests(ContractDeploymentsFixturev2 fixture, ITestOutputHelper output)
        {
            // See Output window -> Tests for fixture deployment logs.
            _contracts = fixture;
            _output = output;
            _xunitlogger = new TestOutputHelperLogger(_output);
        }

        [Fact]
        public async void ShouldDeployNewContract()
        {
            var buyerDeployment = BuyerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 _xunitlogger);
            Func<Task> act = async () => await buyerDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();

            // If buyer deployed ok then its global business partner storage address should have a value
            var bpStorageAddress = await buyerDeployment.BuyerWalletService.BusinessPartnerStorageGlobalQueryAsync().ConfigureAwait(false);
            bpStorageAddress.Should().NotBeNullOrEmpty();
            bpStorageAddress.IsZeroAddress().Should().BeFalse();
        }

        [Fact]
        public void ShouldFailToDeployNewContractWhenMissingWeb3()
        {
            // Give some rubbish addresses for the business partner storage contract
            Action act = () => BuyerDeployment.CreateFromNewDeployment(
                 null,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldFailToDeployNewContractWhenMissingBusinessPartnerAddress(string businessPartnerContractAddress)
        {
            // Give some missing addresses for the existing buyer wallet deployment
            Action act = () => BuyerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 businessPartnerContractAddress,
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldFailToDeployNewContractWhenBadBusinessPartnerAddress()
        {
            // Give a technically valid addess, but not an address for a valid business partner storage contract
            var buyerDeployment1 = BuyerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 "0x32A555F2328e85E489f9a5f03669DC820CE7EBe9", // no business partner storage contract deployed here
                 _xunitlogger);
            Func<Task> act1 = async () => await buyerDeployment1.InitializeAsync();
            await act1.Should().ThrowAsync<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldConnectExistingContract()
        {
            // Deploy a buyer wallet
            var buyerDeployment1 = BuyerDeployment.CreateFromNewDeployment(
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
                 _xunitlogger);
            Func<Task> act = async () => await buyerDeployment1.InitializeAsync();
            await act.Should().NotThrowAsync();

            // Create an additional buyer wallet deployment by connecting to the existing first one
            var buyerDeployment2 = BuyerDeployment.CreateFromConnectExistingContract(
                _contracts.Web3,
                buyerDeployment1.BuyerWalletService.ContractHandler.ContractAddress,
                _xunitlogger);
            Func<Task> act2 = async () => await buyerDeployment2.InitializeAsync();
            await act2.Should().NotThrowAsync();

            buyerDeployment2.Owner.Should().Be(buyerDeployment1.Owner);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldFailToConnectExistingWhenMissingBuyerContractAddress(string buyerContractAddress)
        {
            // Give some missing addresses for the existing buyer wallet deployment
            Action act = () => BuyerDeployment.CreateFromConnectExistingContract(
                 _contracts.Web3,
                 buyerContractAddress,
                 _xunitlogger);
            act.Should().Throw<ContractDeploymentException>().WithMessage("*Failed to set up*");
        }

        [Fact]
        public async void ShouldFailToConnectExistingWhenBadBuyerContractAddress()
        {
            // Give a technically valid addess, but not an address for an existing buyer wallet deployment
            var buyerDeployment = BuyerDeployment.CreateFromConnectExistingContract(
                 _contracts.Web3,
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
                 _contracts.Web3,
                 _contracts.BusinessPartnersDeployment.BusinessPartnerStorageService.ContractHandler.ContractAddress,
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
