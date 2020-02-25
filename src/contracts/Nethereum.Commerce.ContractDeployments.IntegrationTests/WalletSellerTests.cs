using FluentAssertions;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoHelpers;
using static Nethereum.Commerce.Contracts.ContractEnums;
using Buyer = Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [TestCaseOrderer("Nethereum.Commerce.ContractDeployments.IntegrationTests.Config.PriorityOrderer", "Nethereum.Commerce.ContractDeployments.IntegrationTests")]
    [Collection("Contract Deployment Collection")]
    public class WalletSellerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public WalletSellerTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldFailToChangeStatusOnNonExistentPo()
        {
            // PO 12345 shouldn't exist
            await Task.Delay(1);
            Func<Task> act = async () => await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                12345, 1, "SalesOrder1", "SalesOrderItem1");
            act.Should().Throw<SmartContractRevertException>().WithMessage("*PO does not exist*");
        }

        [Fact]
        public async void ShouldFailToChangeStatusOnNonExistentPoItem()
        {
            // This PO exists, but item 255 shouldn't exist
            await Task.Delay(1);
            Func<Task> act = async () => await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                _contracts.PoTest.PoNumber, 255, "SalesOrder1", "SalesOrderItem1");
            act.Should().Throw<SmartContractRevertException>().WithMessage("*PO item does not exist*");
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

        [Fact, TestPriority(10)]
        public async void Order10_ShouldSetPoItemStatusToAccepted()
        {
            // Prepare a new PO
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();

            // Request creation of new PO using the Buyer wallet
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;
            _output.WriteLine($"{poNumberAsBuilt}");

            // Display the as-built PO
            var poActualv1 = (await _contracts.PoStorageService.GetPoQueryAsync(poNumberAsBuilt)).Po;
            DisplaySeparator(_output, "PO v1");
            DisplayPoHeader(_output, poActualv1);
            for (int i = 0; i < poActualv1.PoItems.Count; i++)
            {
                DisplayPoItem(_output, poActualv1.PoItems[i]);
            }

            // Accept the first of the PO items
            byte poItemNumber = 1;
            string salesOrderNumber = "KevinSalesOrder";
            string salesOrderItem = "10";
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, poItemNumber, salesOrderNumber, salesOrderItem);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Check log exists
            var logPoAccepted = txReceiptAccept.DecodeAllEvents<PurchaseItemAcceptedLogEventDTO>().FirstOrDefault();
            logPoAccepted.Should().NotBeNull();
            logPoAccepted.Event.PoItem.Status.Should().Be(PoItemStatus.Accepted);

            // Display the now-updated PO
            var poActualv2 = (await _contracts.WalletSellerService.GetPoQueryAsync(poNumberAsBuilt)).Po.ToStoragePo();
            DisplaySeparator(_output, "PO v2");
            DisplayPoHeader(_output, poActualv2);
            for (int i = 0; i < poActualv2.PoItems.Count; i++)
            {
                DisplayPoItem(_output, poActualv2.PoItems[i]);
            }

            // Check it has been updated correctly
            poActualv2.PoItems[poItemNumber - 1].SoNumber.Should().Be(salesOrderNumber);
            poActualv2.PoItems[poItemNumber - 1].SoItemNumber.Should().Be(salesOrderItem);
            poActualv2.PoItems[poItemNumber - 1].Status.Should().Be(PoItemStatus.Accepted);

            // Store PO number to share with other tests
            _contracts.PoNumber = poNumberAsBuilt;
            _contracts.PoItemNumber = poItemNumber;
        }

        [Fact, TestPriority(20)]
        public async void Order20_ShouldSetPoItemStatusToReadyForGoodsIssue()
        {
            // Retrieve PO
            var poNumber = _contracts.PoNumber;
            byte poItemNumber = _contracts.PoItemNumber;

            // Mark PO item as Ready for Goods Issue            
            var txReceiptReadyForGI = await _contracts.WalletSellerService.SetPoItemReadyForGoodsIssueRequestAndWaitForReceiptAsync(
                poNumber, poItemNumber);
            txReceiptReadyForGI.Status.Value.Should().Be(1);

            // Check log exists
            var logPoReadyForGI = txReceiptReadyForGI.DecodeAllEvents<PurchaseItemReadyForGoodsIssueLogEventDTO>().FirstOrDefault();
            logPoReadyForGI.Should().NotBeNull();
            logPoReadyForGI.Event.PoItem.Status.Should().Be(PoItemStatus.ReadyForGoodsIssue);

            // Get the now-updated PO
            var poActual = (await _contracts.WalletSellerService.GetPoQueryAsync(poNumber)).Po.ToStoragePo();

            // Check it has been updated correctly
            poActual.PoItems[poItemNumber - 1].Status.Should().Be(PoItemStatus.ReadyForGoodsIssue);
        }

        [Fact, TestPriority(30)]
        public async void Order30_ShouldSetPoItemStatusToGoodsIssued()
        {
            // Retrieve PO
            var poNumber = _contracts.PoNumber;
            byte poItemNumber = _contracts.PoItemNumber;

            // Mark PO item as Goods Issued            
            var txReceiptGI = await _contracts.WalletSellerService.SetPoItemGoodsIssuedRequestAndWaitForReceiptAsync(
                poNumber, poItemNumber);
            txReceiptGI.Status.Value.Should().Be(1);

            // Check log exists
            var logPoGI = txReceiptGI.DecodeAllEvents<PurchaseItemGoodsIssuedLogEventDTO>().FirstOrDefault();
            logPoGI.Should().NotBeNull();
            logPoGI.Event.PoItem.Status.Should().Be(PoItemStatus.GoodsIssued);

            // Get the now-updated PO
            var poActual = (await _contracts.WalletSellerService.GetPoQueryAsync(poNumber)).Po.ToStoragePo();

            // Check it has been updated correctly
            poActual.PoItems[poItemNumber - 1].Status.Should().Be(PoItemStatus.GoodsIssued);
        }
    }
}
