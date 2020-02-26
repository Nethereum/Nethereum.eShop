using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Contracts;
using System;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoHelpers;
using static Nethereum.Commerce.Contracts.ContractEnums;
using Buyer = Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class WalletSellerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        private const byte PO_ITEM_NUMBER = 1;
        private const byte PO_ITEM_INDEX = PO_ITEM_NUMBER - 1;
        private const string SALES_ORDER_NUMBER = "SalesOrder01";
        private const string SALES_ORDER_ITEM = "10";

        public WalletSellerTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldFailToSetStatusOnNonExistentPo()
        {
            // PO 12345 shouldn't exist
            await Task.Delay(1);
            Func<Task> act = async () => await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                12345, 1, "SalesOrder1", "SalesOrderItem1");
            act.Should().Throw<SmartContractRevertException>().WithMessage("*PO does not exist*");
        }

        [Theory]
        [InlineData(0)]    // po item number shouldnt ever exist
        [InlineData(255)]  // po item number shouldnt exist on the PO used for test 
        public async void ShouldFailToSetStatusOnNonExistentPoItem(byte poItemNumber)
        {
            // This PO exists, but items specified shouldn't exist
            await Task.Delay(1);
            Func<Task> act = async () => await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                _contracts.PoTest.PoNumber, poItemNumber, "SalesOrder1", "SalesOrderItem1");
            act.Should().Throw<SmartContractRevertException>().WithMessage("*PO item does not exist*");
        }

        [Fact]
        public async void ShouldFailToSetStatusTo06GoodsReceivedBySeller()
        {
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.WalletSellerService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.WalletSellerService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Setting Goods Received by Seller should fail, seller can only set GR after 30 days
            Func<Task> act = async () => await _contracts.WalletSellerService.SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            act.Should().Throw<SmartContractRevertException>().WithMessage("*Seller cannot set goods received: insufficient days passed*");
        }

        [Fact]
        public async void ShouldGetPo()
        {
            // Get test PO created during fixture creation
            var poExpected = _contracts.PoTest;
            var poActual = (await _contracts.WalletSellerService.GetPoQueryAsync(poExpected.PoNumber)).Po;
            CheckEveryPoFieldMatches(poExpected.ToStoragePo(), poActual.ToStoragePo());
        }

        [Fact]
        public async void ShouldGetPoBySellerAndQuote()
        {
            // Get test PO created during fixture creation
            var sellerId = _contracts.PoTest.SellerId;
            var quoteId = _contracts.PoTest.QuoteId;
            var poExpected = _contracts.PoTest;
            var poActual = (await _contracts.WalletSellerService.GetPoBySellerAndQuoteQueryAsync(sellerId, quoteId)).Po;
            CheckEveryPoFieldMatches(poExpected.ToStoragePo(), poActual.ToStoragePo());
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo01Created()
        {
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;
            var po = await GetPoFromPoStorageAndDisplayIt(poNumberAsBuilt);

            // Check PO has been updated correctly
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.Created);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo02Accepted()
        {
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;
            await GetPoFromPoStorageAndDisplayIt(poNumberAsBuilt);

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Check log exists for Accepted
            var logPoAccepted = txReceiptAccept.DecodeAllEvents<PurchaseItemAcceptedLogEventDTO>().FirstOrDefault();
            logPoAccepted.Should().NotBeNull();
            logPoAccepted.Event.PoItem.Status.Should().Be(PoItemStatus.Accepted);

            // Check PO has been updated correctly
            var po = await GetPoFromPoStorageAndDisplayIt(poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].SoNumber.Should().Be(SALES_ORDER_NUMBER);
            po.PoItems[PO_ITEM_INDEX].SoItemNumber.Should().Be(SALES_ORDER_ITEM);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.Accepted);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo03Rejected()
        {
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Rejected            
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemRejectedRequestAndWaitForReceiptAsync(poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Check log exists for Rejected
            var logPoRejected = txReceiptAccept.DecodeAllEvents<PurchaseItemRejectedLogEventDTO>().FirstOrDefault();
            logPoRejected.Should().NotBeNull();
            logPoRejected.Event.PoItem.Status.Should().Be(PoItemStatus.Rejected);

            // Check it has been updated correctly
            var po = await GetPoFromPoStorageAndDisplayIt(poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.Rejected);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo04ReadyForGoodsIssue()
        {
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.WalletSellerService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Check log exists
            var logPoReadyForGI = txReceiptReadyForGI.DecodeAllEvents<PurchaseItemReadyForGoodsIssueLogEventDTO>().FirstOrDefault();
            logPoReadyForGI.Should().NotBeNull();
            logPoReadyForGI.Event.PoItem.Status.Should().Be(PoItemStatus.ReadyForGoodsIssue);

            // Check PO has been updated correctly
            var po = await GetPoFromPoStorageAndDisplayIt(poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.ReadyForGoodsIssue);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo05GoodsIssued()
        {
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.WalletSellerService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.WalletSellerService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Check log exists
            var logPoGI = txReceiptGI.DecodeAllEvents<PurchaseItemGoodsIssuedLogEventDTO>().FirstOrDefault();
            logPoGI.Should().NotBeNull();
            logPoGI.Event.PoItem.Status.Should().Be(PoItemStatus.GoodsIssued);

            // Check PO has been updated correctly
            var po = await GetPoFromPoStorageAndDisplayIt(poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.GoodsIssued);
            var block = await _contracts.Web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(txReceiptGI.BlockNumber);
            var blockTimestamp = block.Timestamp.Value;
            po.PoItems[PO_ITEM_INDEX].GoodsIssuedDate.Should().Be(blockTimestamp);
            po.PoItems[PO_ITEM_INDEX].PlannedEscrowReleaseDate.Should().BeGreaterThan(0, "a Planned Escrow release date should have been assigned");
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo06GoodsReceivedByBuyer()
        {
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.WalletSellerService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.WalletSellerService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Received by the Buyer (so we don't have to wait 30 days)
            var txReceiptGR = await _contracts.WalletBuyerService.SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(
                 poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGR.Status.Value.Should().Be(1);

            // Check log exists
            var logPoGR = txReceiptGR.DecodeAllEvents<PurchaseItemGoodsReceivedLogEventDTO>().FirstOrDefault();
            logPoGR.Should().NotBeNull();
            logPoGR.Event.PoItem.Status.Should().Be(PoItemStatus.GoodsReceived);

            // Check PO has been updated correctly
            var po = await GetPoFromPoStorageAndDisplayIt(poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.GoodsReceived);
            var block = await _contracts.Web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(txReceiptGR.BlockNumber);
            var blockTimestamp = block.Timestamp.Value;
            po.PoItems[PO_ITEM_INDEX].GoodsReceivedDate.Should().Be(blockTimestamp);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo07Completed()
        {
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.WalletSellerService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.WalletSellerService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Received by the Buyer (so we don't have to wait 30 days)
            var txReceiptGR = await _contracts.WalletBuyerService.SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(
                 poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGR.Status.Value.Should().Be(1);

            // Mark PO item as Complete
            var txReceiptCompleted = await _contracts.WalletSellerService.SetPoItemCompletedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptCompleted.Status.Value.Should().Be(1);

            // Check logs exist
            var logPoItemEscrowRelease = txReceiptCompleted.DecodeAllEvents<PurchaseItemEscrowReleasedLogEventDTO>().FirstOrDefault();
            logPoItemEscrowRelease.Should().NotBeNull();
            var logPoItemCompleted = txReceiptCompleted.DecodeAllEvents<PurchaseItemCompletedLogEventDTO>().FirstOrDefault();
            logPoItemCompleted.Should().NotBeNull();
            logPoItemCompleted.Event.PoItem.Status.Should().Be(PoItemStatus.Completed);

            // Check PO has been updated correctly
            var po = await GetPoFromPoStorageAndDisplayIt(poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.Completed);
            var block = await _contracts.Web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(txReceiptCompleted.BlockNumber);
            var blockTimestamp = block.Timestamp.Value;
            po.PoItems[PO_ITEM_INDEX].ActualEscrowReleaseDate.Should().Be(blockTimestamp);
            po.PoItems[PO_ITEM_INDEX].IsEscrowReleased.Should().Be(true);
        }

        private async Task<Po> GetPoFromPoStorageAndDisplayIt(BigInteger poNumber)
        {
            var po = (await _contracts.PoStorageService.GetPoQueryAsync(poNumber)).Po;
            DisplaySeparator(_output, "PO v1");
            DisplayPoHeader(_output, po);
            for (int i = 0; i < po.PoItems.Count; i++)
            {
                DisplayPoItem(_output, po.PoItems[i]);
            }
            return po.ToPurchasingPo();
        }

    }
}
