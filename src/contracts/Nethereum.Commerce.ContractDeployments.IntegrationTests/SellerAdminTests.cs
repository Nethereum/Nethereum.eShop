using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Commerce.Contracts.BuyerWallet;
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
using Seller = Nethereum.Commerce.Contracts.SellerAdmin.ContractDefinition;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Trait("Seller", "")]
    [Collection("CompleteSampleDeploymentFixture")]
    public class SellerAdminTests
    {
        private readonly ITestOutputHelper _output;
        private readonly CompleteSampleDeploymentFixture _contracts;

        private const byte PO_ITEM_NUMBER = 1;
        private const byte PO_ITEM_INDEX = PO_ITEM_NUMBER - 1;
        private const string SALES_ORDER_NUMBER = "SalesOrder01";
        private const string SALES_ORDER_ITEM = "10";

        public SellerAdminTests(CompleteSampleDeploymentFixture fixture, ITestOutputHelper output)
        {
            // CompleteSampleDeploymentFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldFailToSetStatusOnNonExistentPo()
        {
            // PO 12345 shouldn't exist
            var eShopId = await _contracts.Deployment.Eshop.PurchasingService.EShopIdQueryAsync();
            Func<Task> act = async () => await _contracts.Deployment.Seller.SellerAdminService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                eShopId.ConvertToString(), 12345, 1, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(PO_EXCEPTION_NOT_EXIST);
        }

        [Theory]
        [InlineData(0)]    // po item number shouldnt ever exist
        [InlineData(255)]  // po item number shouldnt exist on the PO used for test 
        public async void ShouldFailToSetStatusOnNonExistentPoItem(byte poItemNumber)
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // This PO exists, but items specified shouldn't exist
            Func<Task> act = async () => await _contracts.Deployment.Seller.SellerAdminService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, poItemNumber, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(PO_ITEM_EXCEPTION_NOT_EXIST);
        }

        /// <summary>
        /// Setting Goods Received by an EoA that is not the buyer/PO owner should fail, only PO owner can do this
        /// </summary>
        [Fact]
        public async void ShouldFailToSetStatusTo06GoodsReceivedByNonBuyer()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Setting Goods Received by an EoA that is not the buyer/PO owner should fail, only PO owner can do this    
            // Use preexisting BuyerWallet contract, but with tx executed by the non-buyer ("secondary") user

            // Before starting the secondary user must be whitelisted to use the BuyerWallet contract at all (and un-whitelisted afterwards)
            // bind secondary user as a user of BuyerWallet
            var txReceiptBind = await _contracts.Deployment.Buyer.BuyerWalletService.BindAddressRequestAndWaitForReceiptAsync(_contracts.Web3SecondaryUser.TransactionManager.Account.Address);
            txReceiptBind.Status.Value.Should().Be(1);

            var wbs = new BuyerWalletService(_contracts.Web3SecondaryUser, _contracts.Deployment.Buyer.BuyerWalletService.ContractHandler.ContractAddress);
            Func<Task> act = async () => await wbs.SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(GOODS_RECEIPT_EXCEPTION_NOT_PO_OWNER);

            // unbind secondary user, so they can no longer use BuyerWallet
            var txReceiptUnbind = await _contracts.Deployment.Buyer.BuyerWalletService.UnBindAddressRequestAndWaitForReceiptAsync(_contracts.Web3SecondaryUser.TransactionManager.Account.Address);
            txReceiptUnbind.Status.Value.Should().Be(1);
        }

        /// <summary>
        /// Setting Goods Received by Seller should fail, seller can only set GR after 30 days
        /// </summary>
        [Fact]
        public async void ShouldFailToSetStatusTo06GoodsReceivedBySeller()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Setting Goods Received by Seller should fail, seller can only set GR after 30 days
            Func<Task> act = async () => await _contracts.Deployment.Seller.SellerAdminService.SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            await act.Should().ThrowAsync<SmartContractRevertException>().WithMessage(GOODS_RECEIPT_EXCEPTION_INSUFFICIENT_DAYS);
        }

        [Fact]
        public async void ShouldGetPo()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve PO as-built and check
            var poAsBuilt = await GetPoFromSellerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);
            CheckCreatedPoFieldsMatch(poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(), poNumberAsBuilt);

            // Check PO create event from SellerAdmin contract
            var logQuoteConvertedToPo = txReceiptCreate.DecodeAllEvents<Seller.QuoteConvertedToPoLogEventDTO>().FirstOrDefault();
            logQuoteConvertedToPo.Should().NotBeNull();  // <= Quote converted ok
            // Check event fields
            logQuoteConvertedToPo.Event.EShopId.ConvertToString().Should().Be(poAsRequested.EShopId);
            logQuoteConvertedToPo.Event.QuoteId.Should().Be(poAsRequested.QuoteId);
            logQuoteConvertedToPo.Event.BuyerWalletAddress.Should().Be(poAsRequested.BuyerWalletAddress);
        }

        [Fact]
        public async void ShouldGetPoBySellerAndQuote()
        {
            // Prepare a new PO and create it            
            var quoteId = GetRandomInt();
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId);
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve PO as-built using Seller and Quote, and check
            var poAsBuilt = (await _contracts.Deployment.Seller.SellerAdminService.GetPoByEshopIdAndQuoteQueryAsync(poAsRequested.EShopId, quoteId)).Po;
            CheckCreatedPoFieldsMatch(poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(), poNumberAsBuilt);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo01Created()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;
            var po = await GetPoFromSellerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);

            // Check PO has been updated correctly
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.Created);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo02Accepted()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;
            await GetPoFromSellerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Check log exists for Accepted
            var logPoAccepted = txReceiptAccept.DecodeAllEvents<PurchaseItemAcceptedLogEventDTO>().FirstOrDefault();
            logPoAccepted.Should().NotBeNull();
            logPoAccepted.Event.PoItem.Status.Should().Be(PoItemStatus.Accepted);

            // Check PO has been updated correctly
            var po = await GetPoFromSellerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].SoNumber.Should().Be(SALES_ORDER_NUMBER);
            po.PoItems[PO_ITEM_INDEX].SoItemNumber.Should().Be(SALES_ORDER_ITEM);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.Accepted);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo03Rejected()
        {
            // Prepare a new PO and create it
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Rejected            
            var txReceiptAccept = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemRejectedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Check log exists for Rejected
            var logPoRejected = txReceiptAccept.DecodeAllEvents<PurchaseItemRejectedLogEventDTO>().FirstOrDefault();
            logPoRejected.Should().NotBeNull();
            logPoRejected.Event.PoItem.Status.Should().Be(PoItemStatus.Rejected);

            // Check it has been updated correctly
            var po = await GetPoFromSellerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.Rejected);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo04ReadyForGoodsIssue()
        {
            // Prepare a new PO and create it
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Check log exists
            var logPoReadyForGI = txReceiptReadyForGI.DecodeAllEvents<PurchaseItemReadyForGoodsIssueLogEventDTO>().FirstOrDefault();
            logPoReadyForGI.Should().NotBeNull();
            logPoReadyForGI.Event.PoItem.Status.Should().Be(PoItemStatus.ReadyForGoodsIssue);

            // Check PO has been updated correctly
            var po = await GetPoFromSellerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.ReadyForGoodsIssue);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo05GoodsIssued()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Check log exists
            var logPoGI = txReceiptGI.DecodeAllEvents<PurchaseItemGoodsIssuedLogEventDTO>().FirstOrDefault();
            logPoGI.Should().NotBeNull();
            logPoGI.Event.PoItem.Status.Should().Be(PoItemStatus.GoodsIssued);

            // Check PO has been updated correctly
            var po = await GetPoFromSellerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);
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
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Received by the Buyer (so we don't have to wait 30 days)
            var txReceiptGR = await _contracts.Deployment.Buyer.BuyerWalletService.SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(
                 poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGR.Status.Value.Should().Be(1);

            // Check log exists
            var logPoGR = txReceiptGR.DecodeAllEvents<PurchaseItemGoodsReceivedLogEventDTO>().FirstOrDefault();
            logPoGR.Should().NotBeNull();
            logPoGR.Event.PoItem.Status.Should().Be(PoItemStatus.GoodsReceived);

            // Check PO has been updated correctly
            var po = await GetPoFromSellerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.GoodsReceived);
            var block = await _contracts.Web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(txReceiptGR.BlockNumber);
            var blockTimestamp = block.Timestamp.Value;
            po.PoItems[PO_ITEM_INDEX].GoodsReceivedDate.Should().Be(blockTimestamp);
        }

        [Fact]
        public async void ShouldSetPoItemStatusTo07Completed()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.Buyer.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Mark PO item as Accepted
            var txReceiptAccept = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER, SALES_ORDER_NUMBER, SALES_ORDER_ITEM);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGI.Status.Value.Should().Be(1);

            // Mark PO item as Goods Received by the Buyer (so we don't have to wait 30 days)
            var txReceiptGR = await _contracts.Deployment.Buyer.BuyerWalletService.SetPoItemGoodsReceivedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptGR.Status.Value.Should().Be(1);

            // Mark PO item as Complete
            var txReceiptCompleted = await _contracts.Deployment.Seller.SellerAdminService.SetPoItemCompletedRequestAndWaitForReceiptAsync(
                poAsRequested.EShopId, poNumberAsBuilt, PO_ITEM_NUMBER);
            txReceiptCompleted.Status.Value.Should().Be(1);

            // Check logs exist
            var logPoItemEscrowRelease = txReceiptCompleted.DecodeAllEvents<PurchaseItemEscrowReleasedLogEventDTO>().FirstOrDefault();
            logPoItemEscrowRelease.Should().NotBeNull();
            var logPoItemCompleted = txReceiptCompleted.DecodeAllEvents<PurchaseItemCompletedLogEventDTO>().FirstOrDefault();
            logPoItemCompleted.Should().NotBeNull();
            logPoItemCompleted.Event.PoItem.Status.Should().Be(PoItemStatus.Completed);

            // Check PO has been updated correctly
            var po = await GetPoFromSellerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);
            po.PoItems[PO_ITEM_INDEX].Status.Should().Be(PoItemStatus.Completed);
            var block = await _contracts.Web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(txReceiptCompleted.BlockNumber);
            var blockTimestamp = block.Timestamp.Value;
            po.PoItems[PO_ITEM_INDEX].ActualEscrowReleaseDate.Should().Be(blockTimestamp);
            po.PoItems[PO_ITEM_INDEX].IsEscrowReleased.Should().Be(true);
        }

        private async Task<Buyer.Po> CreateBuyerPoAsync(uint quoteId)
        {
            return CreatePoForPurchasingContracts(
                buyerUserAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerReceiverAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerWalletAddress: _contracts.Deployment.Buyer.BuyerWalletService.ContractHandler.ContractAddress.ToLowerInvariant(),
                eShopId: _contracts.Deployment.Eshop.EshopId,
                sellerId: _contracts.Deployment.Seller.SellerId,
                currencySymbol: await _contracts.Deployment.MockDaiService.SymbolQueryAsync(),
                currencyAddress: _contracts.Deployment.MockDaiService.ContractHandler.ContractAddress.ToLowerInvariant(),
                quoteId).ToBuyerPo();
        }

        private async Task<Seller.Po> GetPoFromSellerContractAndDisplayAsync(string eShopId, BigInteger poNumber, string title = "PO")
        {
            var po = (await _contracts.Deployment.Seller.SellerAdminService.GetPoQueryAsync(eShopId, poNumber)).Po;
            DisplaySeparator(_output, title);
            DisplayPoHeader(_output, po.ToStoragePo());
            for (int i = 0; i < po.PoItems.Count; i++)
            {
                DisplayPoItem(_output, po.ToStoragePo().PoItems[i]);
            }
            return po;
        }
    }
}
