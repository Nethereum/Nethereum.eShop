using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.MockDai;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Contracts;
using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoTestHelpers;
using static Nethereum.Commerce.Contracts.ContractEnums;
using Buyer = Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.StandardTokenEIP20;
using Nethereum.Commerce.Contracts.SellerAdmin;
using Nethereum.Commerce.Contracts.BuyerWallet;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Trait("Shop", "")]
    [Collection("Contract Deployment Collection")]
    public class FundingTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        private const byte PO_ITEM_NUMBER = 1;
        private const byte PO_ITEM_INDEX = PO_ITEM_NUMBER - 1;
        private const string SALES_ORDER_NUMBER = "SalesOrder01";
        private const string SALES_ORDER_ITEM = "10";

        public FundingTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldCreateNewPoAndTransferFunds()
        {
            // Prepare a new PO            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);

            //----------------------------------------------------------
            // BEFORE PO RAISED
            //----------------------------------------------------------
            // Balance of Web3, before test starts (check account running these tests has enough funds to pay for the PO)
            StandardTokenService sts = new StandardTokenService(_contracts.Web3, poAsRequested.CurrencyAddress);
            var totalPoValue = poAsRequested.GetTotalCurrencyValue();
            var web3AddressBalance = await sts.BalanceOfQueryAsync(_contracts.Web3.TransactionManager.Account.Address);
            web3AddressBalance.Should().BeGreaterOrEqualTo(totalPoValue, "the Web3 account must be able to pay for whole PO");
            _output.WriteLine($"PO: {poAsRequested.PoNumber}  total value {await totalPoValue.PrettifyAsync(sts)}");

            // Balance of BuyerWallet, before test starts
            var buyerWalletBalance = await sts.BalanceOfQueryAsync(poAsRequested.BuyerWalletAddress);
            _output.WriteLine($"Wallet Buyer balance before test: {await buyerWalletBalance.PrettifyAsync(sts)}");

            // Test setup - transfer required funds from current Web3 acccount to wallet buyer
            var txTransfer = await sts.TransferRequestAndWaitForReceiptAsync(poAsRequested.BuyerWalletAddress, totalPoValue);
            txTransfer.Status.Value.Should().Be(1);

            // Balance of BuyerWallet, before PO raised   
            buyerWalletBalance = await sts.BalanceOfQueryAsync(poAsRequested.BuyerWalletAddress);
            _output.WriteLine($"Wallet Buyer balance after receiving funding from Web3 account: {await buyerWalletBalance.PrettifyAsync(sts)}");

            // Balance of Funding, before PO raised   
            var fundingBalanceBefore = await sts.BalanceOfQueryAsync(_contracts.Deployment.FundingServiceLocal.ContractHandler.ContractAddress);
            _output.WriteLine($"Funding balance before PO: {await fundingBalanceBefore.PrettifyAsync(sts)}");

            //----------------------------------------------------------
            // RAISE PO 
            //----------------------------------------------------------
            // Create PO on-chain
            // NB this approves token transfer from WALLET BUYER contract (NOT msg.sender == current web3 account) to FUNDING contract
            var txReceiptCreate = await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);
            _output.WriteLine($"... PO created ...");

            //----------------------------------------------------------
            // AFTER PO RAISED
            //----------------------------------------------------------
            // Balance of BuyerWallet, after PO raised   
            buyerWalletBalance = await sts.BalanceOfQueryAsync(poAsRequested.BuyerWalletAddress);
            _output.WriteLine($"Wallet Buyer balance after PO: {await buyerWalletBalance.PrettifyAsync(sts)}");

            // Balance of Funding, after PO raised   
            var fundingBalanceAfter = await sts.BalanceOfQueryAsync(_contracts.Deployment.FundingServiceLocal.ContractHandler.ContractAddress);
            _output.WriteLine($"Funding balance after PO: {await fundingBalanceAfter.PrettifyAsync(sts)}");

            // Check
            var diff = fundingBalanceAfter - fundingBalanceBefore;
            diff.Should().Be(totalPoValue, "funding contract should have increased in value by the PO value");
        }

        [Fact]
        public async void ShouldRejectPoItemAndRefundBuyer()
        {
            // Prepare a new PO            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);

            // Test setup - transfer required funds from current Web3 acccount to wallet buyer
            StandardTokenService sts = new StandardTokenService(_contracts.Web3, poAsRequested.CurrencyAddress);
            var totalPoValue = poAsRequested.GetTotalCurrencyValue();
            var txTransfer = await sts.TransferRequestAndWaitForReceiptAsync(poAsRequested.BuyerWalletAddress, totalPoValue);
            txTransfer.Status.Value.Should().Be(1);

            // Create PO on-chain
            // NB: this approves token transfer from WALLET BUYER contract (NOT msg.sender == current web3 account) to FUNDING contract
            var txReceiptCreate = await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);
            _output.WriteLine($"... PO created ...");

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // NB: Refunds go to the PO buyer wallet address (not the po buyer address from the PO header, which represents the user)
            // Balance of PO buyer address before refund
            var poBuyerWalletAddressBalanceBefore = await sts.BalanceOfQueryAsync(poAsRequested.BuyerWalletAddress);
            _output.WriteLine($"PO buyer wallet address balance before refund: {await poBuyerWalletAddressBalanceBefore.PrettifyAsync(sts)}");

            // Do the refund (achieved by marking the PO item as rejected)
            var txReceiptPoItemReject = await _contracts.Deployment.SellerAdminService.SetPoItemRejectedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptPoItemReject.Status.Value.Should().Be(1);
            var poItemValue = poAsRequested.PoItems[PO_ITEM_INDEX].CurrencyValue;
            _output.WriteLine($"... PO item rejected with value {await poItemValue.PrettifyAsync(sts)} ...");

            // Check log exists for Escrow refund
            var logPoItemReject = txReceiptPoItemReject.DecodeAllEvents<PurchaseItemEscrowRefundedLogEventDTO>().FirstOrDefault();
            logPoItemReject.Should().NotBeNull();

            // Balance of PO buyer wallet address after PO item rejection
            var poBuyerWalletAddressBalanceAfter = await sts.BalanceOfQueryAsync(poAsRequested.BuyerWalletAddress);
            _output.WriteLine($"PO buyer wallet address balance after refund: {await poBuyerWalletAddressBalanceAfter.PrettifyAsync(sts)}");

            // Checks
            var diff = poBuyerWalletAddressBalanceAfter - poBuyerWalletAddressBalanceBefore;
            diff.Should().Be(poItemValue, "PO buyer wallet should have increased by value of the PO item");
        }

        [Fact]
        public async void ShouldCompletePoItemAndPaySellerLessFees()
        {
            // Prepare a new PO            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);

            // Test setup - transfer required funds from current Web3 acccount to wallet buyer
            BuyerWalletService bws = _contracts.Deployment.BuyerWalletService;
            SellerAdminService sas = _contracts.Deployment.SellerAdminService;
            StandardTokenService sts = new StandardTokenService(_contracts.Web3, poAsRequested.CurrencyAddress);
            var totalPoValue = poAsRequested.GetTotalCurrencyValue();
            var txTransfer = await sts.TransferRequestAndWaitForReceiptAsync(poAsRequested.BuyerWalletAddress, totalPoValue);
            txTransfer.Status.Value.Should().Be(1);

            // Create PO on-chain
            // NB: this approves token transfer from BUYER WALLET contract (NOT msg.sender == current web3 account) to FUNDING contract
            var txReceiptCreate = await bws.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);
            _output.WriteLine($"... PO created ...");

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // NB: Payment goes to the SellerAdmin address 
            var sellerAdminBalanceBefore = await sts.BalanceOfQueryAsync(_contracts.Deployment.SellerAdminService.ContractHandler.ContractAddress);
            _output.WriteLine($"SellerAdmin balance before completion: {await sellerAdminBalanceBefore.PrettifyAsync(sts)}");

            // Process PO item through to completion (completion step will release funds)
            // Mark PO item as Accepted
            var txReceiptAccept = await sas.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await sas.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await sas.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Received by the Buyer (so we don't have to wait 30 days)
            var txReceiptGR = await bws.SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(
                 poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGR.Status.Value.Should().Be(1);

            // Mark PO item as Complete
            var txReceiptCompleted = await sas.SetPoItemCompletedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptCompleted.Status.Value.Should().Be(1);
            var poItemValue = poAsRequested.PoItems[PO_ITEM_INDEX].CurrencyValue;
            _output.WriteLine($"... PO item completed with value {await poItemValue.PrettifyAsync(sts)} ...");

            // Check log exists for Escrow release
            var logPoItemCompleted = txReceiptCompleted.DecodeAllEvents<PurchaseItemEscrowReleasedLogEventDTO>().FirstOrDefault();
            logPoItemCompleted.Should().NotBeNull();

            // Balance of PO buyer address after PO item rejection
            var sellerAdminBalanceAfter = await sts.BalanceOfQueryAsync(_contracts.Deployment.SellerAdminService.ContractHandler.ContractAddress);
            _output.WriteLine($"SellerAdmin balance after completion: {await sellerAdminBalanceAfter.PrettifyAsync(sts)}");

            // Checks must include fees
            var feeBasisPoints = await _contracts.Deployment.PurchasingServiceLocal.GetFeeBasisPointsQueryAsync();
            var fee = poItemValue * feeBasisPoints / 10000;
            var diff = sellerAdminBalanceAfter - sellerAdminBalanceBefore;
            diff.Should().Be(poItemValue - fee, "SellerAdmin contract address should increase by PO item value minus fee");
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
