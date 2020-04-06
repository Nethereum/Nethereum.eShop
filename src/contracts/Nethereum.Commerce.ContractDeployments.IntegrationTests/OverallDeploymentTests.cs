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
    [Collection("Contract Deployment Collection")]
    public class OverallDeploymentTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public OverallDeploymentTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldHaveDeployedAndConfiguredAllContracts()
        {
            // If all contracts deployed and configured ok, then...
            // ...the PO storage contract should be configured to point to the eternal storage contract.
            var actualEternalStorageAddressHeldAgainstPoStorage = await _contracts.Deployment.PoStorageServiceLocal.EternalStorageQueryAsync();
            var expectedEternalStorageAddress = _contracts.Deployment.EternalStorageServiceLocal.ContractHandler.ContractAddress;
            actualEternalStorageAddressHeldAgainstPoStorage.Should().Be(expectedEternalStorageAddress);

            // ...the funding contract should be configured to point to the business partner storage contract.
            var actualBusinessPartnerStorageAddressHeldAgainstFunding = await _contracts.Deployment.FundingServiceLocal.BpStorageGlobalQueryAsync();
            var expectedBusinessPartnerAddress = _contracts.Deployment.BusinessPartnerStorageServiceGlobal.ContractHandler.ContractAddress;
            actualBusinessPartnerStorageAddressHeldAgainstFunding.Should().Be(expectedBusinessPartnerAddress);

            // ... the buyer wallet should be configured to point to the business partner storage contract.
            var actualBusinessPartnerStorageAddressHeldAgainstBuyerWallet = await _contracts.Deployment.BuyerWalletService.BpStorageGlobalQueryAsync();
            actualBusinessPartnerStorageAddressHeldAgainstBuyerWallet.Should().Be(expectedBusinessPartnerAddress);

            // ... the seller admin should be configured to point to the business partner storage contract.
            var actualBusinessPartnerStorageAddressHeldAgainstSellerAdmin = await _contracts.Deployment.SellerAdminService.BpStorageGlobalQueryAsync();
            actualBusinessPartnerStorageAddressHeldAgainstSellerAdmin.Should().Be(expectedBusinessPartnerAddress);

            // ... the seller admin should be configured to have a seller id.
            var actualSellerIdString = (await _contracts.Deployment.SellerAdminService.SellerIdQueryAsync()).ConvertToString();
            var expectedSellerIdString = _contracts.Deployment.ContractNewDeploymentConfig.Seller.SellerId;
            actualSellerIdString.Should().Be(expectedSellerIdString);

            // ... and that seller id should have a master data entry in business partner storage.            
            var actualSellerIdBytes = actualSellerIdString.ConvertToBytes32();
            var actualSellerIdRecordFromBusinessPartnerStorage = (await _contracts.Deployment.BusinessPartnerStorageServiceGlobal.GetSellerQueryAsync(actualSellerIdBytes)).Seller;
            actualSellerIdRecordFromBusinessPartnerStorage.IsActive.Should().Be(true);
            actualSellerIdRecordFromBusinessPartnerStorage.SellerDescription.Should().Be(
                _contracts.Deployment.ContractNewDeploymentConfig.Seller.SellerDescription);
        }

        [Fact]
        public async void ShouldHaveDeployedMockContracts()
        {
            // If mock contracts deployed ok, then a MockDai contract should exist
            var totalSupply = await _contracts.Deployment.MockDaiService.TotalSupplyQueryAsync();
            totalSupply.Should().BeGreaterThan(1);
            var dec = await _contracts.Deployment.MockDaiService.DecimalsQueryAsync();
            dec.Should().BeGreaterThan(0);
            var totalSupplyFactored = totalSupply / BigInteger.Pow(10, dec);
            _output.WriteLine($"MockDai Total Supply = {totalSupplyFactored.ToString("N0")}");
        }

        [Fact]
        public async void ShouldNotAllowDuplicateEshopDeployment()
        {
            // Prevent a second Purchasing.sol contract being deployed that is trying to 
            // use an existing eShop that is already pointing at another Purchasing.sol.
            // This is checked for when Purchasing.sol is configured.
            var purchasingDeployment = new PurchasingDeployment()
            {
                ContractAddressOfRegistryGlobal = _contracts.Deployment.AddressRegistryServiceGlobal.ContractHandler.ContractAddress,
                ContractAddressOfRegistryLocal = _contracts.Deployment.AddressRegistryServiceLocal.ContractHandler.ContractAddress,
                EShopIdString = _contracts.Deployment.ContractNewDeploymentConfig.Eshop.EShopId
            };
            var psl = await PurchasingService.DeployContractAndGetServiceAsync(
               _contracts.Web3, purchasingDeployment).ConfigureAwait(false);

            Func<Task> act = async () => await psl.ConfigureRequestAndWaitForReceiptAsync(
                ContractDeployment.CONTRACT_NAME_BUSINESS_PARTNER_STORAGE_GLOBAL,
                ContractDeployment.CONTRACT_NAME_PO_STORAGE_LOCAL,
                ContractDeployment.CONTRACT_NAME_FUNDING_LOCAL);
            act.Should().Throw<SmartContractRevertException>().WithMessage(PO_EXCEPTION_CHECK_ESHOP_MASTER_DATA); 
        }
    }
}
