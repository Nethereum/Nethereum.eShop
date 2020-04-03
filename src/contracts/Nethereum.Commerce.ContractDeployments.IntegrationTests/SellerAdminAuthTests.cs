using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Purchasing;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Commerce.Contracts.SellerAdmin;
using Nethereum.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;
using Buyer = Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition;
using Purchasing = Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class SellerAdminAuthTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public SellerAdminAuthTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldNotBeAbleToSetPoItemStatusWhenNotSellerAdminOwner()
        {
            // Try to set a PO item status by a non-authorised user, it should fail            
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceipt = await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceipt.Status.Value.Should().Be(1);

            // Check PO create events
            var logPoCreated = txReceipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Attempt to mark PO item as accepted using preexisting SellerAdmin contract, but with tx executed by the non-authorised ("secondary") user
            var wss = new SellerAdminService(_contracts.Web3SecondaryUser, _contracts.Deployment.SellerAdminService.ContractHandler.ContractAddress);
            Func<Task> act = async () => await wss.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, 1, "SalesOrder1", "Item1");
            act.Should().Throw<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_OWNER);
        }

        [Fact]
        public async void ShouldNotAllowNonEshopToCallEmitEventForNewPo()
        {
            // SellerAdmin has a fn emitEventForNewPo that is called by Purchasing.sol when a shop successfully
            // raises a new PO. If a non-shop address tries to call the fn directly then the tx should revert.

            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Direct call of the SellerAdmin.sol function EmitEventForNewPoRequest should fail, since the caller is not an eShop
            Func<Task> act = async () => await _contracts.Deployment.SellerAdminService.EmitEventForNewPoRequestAndWaitForReceiptAsync(poAsRequested.ToSellerPo());
            act.Should().Throw<SmartContractRevertException>().WithMessage(ESHOP_EXCEPTION_FUNCTION_ONLY_CALLABLE_BY_ESHOP);
        }

        private async Task<Buyer.Po> CreateBuyerPoAsync(uint quoteId)
        {
            return CreatePoForPurchasingContracts(
                buyerUserAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerReceiverAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerWalletAddress: _contracts.Deployment.BuyerWalletService.ContractHandler.ContractAddress.ToLowerInvariant(),
                eShopId: _contracts.Deployment.ContractNewDeploymentConfig.Eshop.EShopId,
                sellerId: _contracts.Deployment.ContractNewDeploymentConfig.Seller.SellerId,
                currencySymbol: await _contracts.Deployment.MockDaiService.SymbolQueryAsync(),
                currencyAddress: _contracts.Deployment.MockDaiService.ContractHandler.ContractAddress.ToLowerInvariant(),
                quoteId).ToBuyerPo();
        }
    }
}
