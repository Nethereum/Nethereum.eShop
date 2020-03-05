using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Purchasing;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Commerce.Contracts.WalletSeller;
using Nethereum.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;
using Buyer = Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;
using Purchasing = Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class WalletSellerAuthTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public WalletSellerAuthTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldNotBeAbleToSetPoItemStatusWhenNotWalletSellerOwner()
        {
            // Try to set a PO item status by a non-authorised user, it should fail            
            // Prepare a new PO
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId);

            // Request creation of new PO
            var txReceipt = await _contracts.Deployment.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceipt.Status.Value.Should().Be(1);

            // Check PO create events
            var logPoCreated = txReceipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Attempt to mark PO item as accepted using preexisting WalletSeller contract, but with tx executed by the non-authorised ("secondary") user
            var wss = new WalletSellerService(_contracts.Web3SecondaryUser, _contracts.Deployment.WalletSellerService.ContractHandler.ContractAddress);
            Func<Task> act = async () => await wss.SetPoItemAcceptedRequestAndWaitForReceiptAsync(poNumberAsBuilt, 1, "SalesOrder1", "Item1");
            act.Should().Throw<SmartContractRevertException>().WithMessage(AUTH_EXCEPTION_ONLY_OWNER);
        }

        private async Task<Buyer.Po> CreateBuyerPoAsync(uint quoteId)
        {
            return CreatePoForPurchasingContracts(
                buyerAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                receiverAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerWalletAddress: _contracts.Deployment.WalletBuyerService.ContractHandler.ContractAddress.ToLowerInvariant(),
                currencySymbol: await _contracts.Deployment.MockDaiService.SymbolQueryAsync(),
                currencyAddress: _contracts.Deployment.MockDaiService.ContractHandler.ContractAddress.ToLowerInvariant(),
                quoteId).ToBuyerPo();
        }
    }
}
