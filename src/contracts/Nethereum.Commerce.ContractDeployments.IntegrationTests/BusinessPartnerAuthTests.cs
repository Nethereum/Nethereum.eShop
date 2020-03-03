using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage;
using Nethereum.Commerce.Contracts.BusinessPartnerStorage.ContractDefinition;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoHelpers;

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
        public async void ShouldFailToStoreSellerWhenNotOwner()
        {
            // Try to store a Seller sent by a non-authorised user, it should fail            
            // Create a Seller to store
            var sellerContractAddress = _contracts.Deployment.WalletSellerService.ContractHandler.ContractAddress;
            var sellerExpected = new Seller()
            {
                SellerId = "SellerToTest",
                SellerDescription = _contracts.Deployment.ContractDeploymentConfig.EShopDescription,
                ContractAddress = sellerContractAddress,
                ApproverAddress = _contracts.Deployment.ContractDeploymentConfig.EShopApproverAddress,
                IsActive = true
            };

            // Store Seller using preexisting bp storage service contract, but with tx executed by the non-authorised ("secondary") user
            await Task.Delay(1);
            var bpss = new BusinessPartnerStorageService(_contracts.Web3SecondaryUser, _contracts.Deployment.BusinessPartnerStorageService.ContractHandler.ContractAddress);
            Func<Task> act = async () => await bpss.SetSellerRequestAndWaitForReceiptAsync(sellerExpected);
            act.Should().Throw<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_REGISTERED);         
        }
    }
}
