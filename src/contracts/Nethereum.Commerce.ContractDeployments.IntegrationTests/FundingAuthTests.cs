using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts.Funding;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class FundingAuthTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;
   
        public FundingAuthTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldNotBeAbleToChangeOwnerWhenNotOwner()
        {
            var fs = new FundingService(_contracts.Web3SecondaryUser, _contracts.Deployment.FundingServiceLocal.ContractHandler.ContractAddress);
            Func<Task> act = async () => await fs.TransferOwnershipRequestAndWaitForReceiptAsync(_contracts.Web3SecondaryUser.TransactionManager.Account.Address);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_OWNER);
        }

        [Fact]
        public async void ShouldNotBeAbleToTransferFundsWhenNotRegisteredCaller()
        {
            // Try to transfer funds for a PO using preexisting Funding contract, but with tx executed by the non-authorised ("secondary") user
            // PO may or may not exist, exception thrown will be before PO existence check
            var fs = new FundingService(_contracts.Web3SecondaryUser, _contracts.Deployment.FundingServiceLocal.ContractHandler.ContractAddress);
            Func<Task> act1 = async () => await fs.TransferInFundsForPoFromBuyerWalletRequestAndWaitForReceiptAsync(1);
            await act1.Should().ThrowAsync<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_REGISTERED);

            Func<Task> act2 = async () => await fs.TransferOutFundsForPoItemToBuyerRequestAndWaitForReceiptAsync(1, 1);
            await act2.Should().ThrowAsync<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_REGISTERED);

            Func<Task> act3 = async () => await fs.TransferOutFundsForPoItemToSellerRequestAndWaitForReceiptAsync(1, 1);
            await act3.Should().ThrowAsync<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_REGISTERED);
        }
    }
}
