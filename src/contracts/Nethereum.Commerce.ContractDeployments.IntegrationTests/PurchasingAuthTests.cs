using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Purchasing;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;
using Purchasing = Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class PurchasingAuthTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public PurchasingAuthTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldNotBeAbleToCreatePoWhenNotRegisteredCaller()
        {
            // Try to create a PO sent by a non-authorised user, it should fail            
            // Prepare a new PO            
            Purchasing.Po poAsRequested = await CreatePurchasingPoAsync(quoteId: GetRandomInt(), eShopId: GetRandomString());
            var signature = poAsRequested.ToBuyerPo().GetSignatureBytes(_contracts.Web3);

            // Request creation of new PO using preexisting Purchasing contract, but with tx executed by the non-authorised ("secondary") user
            await Task.Delay(1);
            var ps = new PurchasingService(_contracts.Web3SecondaryUser, _contracts.Deployment.PurchasingService.ContractHandler.ContractAddress);
            Func<Task> act = async () => await ps.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            act.Should().Throw<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_REGISTERED);
        }

        private async Task<Purchasing.Po> CreatePurchasingPoAsync(uint quoteId, string eShopId)
        {
            Storage.Po po = CreatePoForPurchasingContracts(
                buyerUserAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerReceiverAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerWalletAddress: _contracts.Deployment.WalletBuyerService.ContractHandler.ContractAddress.ToLowerInvariant(),
                eShopId: eShopId,
                currencySymbol: await _contracts.Deployment.MockDaiService.SymbolQueryAsync(),
                currencyAddress: _contracts.Deployment.MockDaiService.ContractHandler.ContractAddress.ToLowerInvariant(),
                quoteId);
            return po.ToPurchasingPo();
        }
    }
}
