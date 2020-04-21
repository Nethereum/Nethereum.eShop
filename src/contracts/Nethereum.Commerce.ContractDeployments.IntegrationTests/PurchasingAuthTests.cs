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
    [Trait("Shop", "")]
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
        public async void ShouldNotBeAbleToCreatePoDirectlyUsingNonWalletBuyerAddress()
        {
            // Try to create a PO directly against the Purchasing contract, it should fail if the POs buyerWalletAddress does not match msg.sender           
            // Prepare a new PO            
            Purchasing.Po poAsRequested = await CreatePurchasingPoAsync(quoteId: GetRandomInt(), eShopId: GetRandomString());
            var signature = poAsRequested.ToBuyerPo().GetSignatureBytes(_contracts.Web3);

            //  Tx executed by the secondary user who 1) isn't the POs buyerWalletAddress and 2) doesn't own the Purchasing contract 
            var ps = new PurchasingService(_contracts.Web3SecondaryUser, _contracts.Deployment.PurchasingServiceLocal.ContractHandler.ContractAddress);
            Func<Task> act = async () => await ps.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_BUYER_OR_PURCH_OWNER);
        }

        private async Task<Purchasing.Po> CreatePurchasingPoAsync(uint quoteId, string eShopId)
        {
            Storage.Po po = CreatePoForPurchasingContracts(
                buyerUserAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerReceiverAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerWalletAddress: _contracts.Deployment.BuyerWalletService.ContractHandler.ContractAddress.ToLowerInvariant(),
                eShopId: eShopId,
                sellerId: _contracts.Deployment.ContractNewDeploymentConfig.Seller.SellerId,
                currencySymbol: await _contracts.Deployment.MockDaiService.SymbolQueryAsync(),
                currencyAddress: _contracts.Deployment.MockDaiService.ContractHandler.ContractAddress.ToLowerInvariant(),
                quoteId);
            return po.ToPurchasingPo();
        }
    }
}
