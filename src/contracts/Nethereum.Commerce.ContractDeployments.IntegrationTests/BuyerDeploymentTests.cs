using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Deployment;
using Nethereum.Commerce.Contracts.Purchasing;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection v2")]
    public class BuyerDeploymentTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixturev2 _contracts;
        private readonly TestOutputHelperLogger _xunitlogger;

        public BuyerDeploymentTests(ContractDeploymentsFixturev2 fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
            _xunitlogger = new TestOutputHelperLogger(_output);
        }

        [Fact]
        public async void ShouldDeployNewContract()
        {
            BuyerDeployment buyerDeployment = new BuyerDeployment(
                _contracts.Web3,
                "",
                "",
                _xunitlogger);
            Func<Task> act = async () => await buyerDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();

            // above should not throw

            //// If all contracts deployed and configured ok, then...
            //// ...the PO storage contract should be configured to point to the eternal storage contract.
            //var actualEternalStorageAddressHeldAgainstPoStorage = await _contracts.Deployment.PoStorageServiceLocal.EternalStorageQueryAsync();
            //var expectedEternalStorageAddress = _contracts.Deployment.EternalStorageServiceLocal.ContractHandler.ContractAddress;
            //actualEternalStorageAddressHeldAgainstPoStorage.Should().Be(expectedEternalStorageAddress);

            //// ...the funding contract should be configured to point to the business partner storage contract.
            //var actualBusinessPartnerStorageAddressHeldAgainstFunding = await _contracts.Deployment.FundingServiceLocal.BpStorageGlobalQueryAsync();
            //var expectedBusinessPartnerAddress = _contracts.Deployment.BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress;
            //actualBusinessPartnerStorageAddressHeldAgainstFunding.Should().Be(expectedBusinessPartnerAddress);
        }

        [Fact]
        public async void ShouldConnectExistingContract()
        {
            // Deploy a buyer wallet

            // Get its address

            // try to connect to it

            BuyerDeployment buyerDeployment = new BuyerDeployment(
                _contracts.Web3,
                "",
                _xunitlogger);
            Func<Task> act = async () => await buyerDeployment.InitializeAsync();
            await act.Should().NotThrowAsync();

            // above should not throw

        }
    }
}
