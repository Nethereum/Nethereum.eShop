using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Contracts;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoHelpers;
using Buyer = Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using static Nethereum.Commerce.Contracts.ContractEnums;
using System.Threading.Tasks;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
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
        public async void ShouldSetPoItemStatusToAccepted()
        {
            // Prepare a new PO
            string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPo(0, approverAddress, quoteId).ToBuyerPo(); //po number assigned by contract

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
            byte itemNumber = 1;
            string salesOrderNumber = "KevinSalesOrder";
            string salesOrderItem = "10";
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, itemNumber, salesOrderNumber, salesOrderItem);
            txReceiptAccept.Status.Value.Should().Be(1);

            // Check log exists
            var logPoAccepted = txReceiptAccept.DecodeAllEvents<PurchaseItemAcceptedLogEventDTO>().FirstOrDefault();
            logPoAccepted.Should().NotBeNull();
            logPoAccepted.Event.PoItem.Status.Should().Be(PoItemStatus.Accepted);

            // Display the now-updated, as-built, PO
            var poActualv2 = (await _contracts.WalletSellerService.GetPoQueryAsync(poNumberAsBuilt)).Po.ToStoragePo();
            DisplaySeparator(_output, "PO v2");
            DisplayPoHeader(_output, poActualv2);
            for (int i = 0; i < poActualv2.PoItems.Count; i++)
            {
                DisplayPoItem(_output, poActualv2.PoItems[i]);
            }

            // Check it has been updated correctly
            poActualv2.PoItems[itemNumber - 1].SoNumber.Should().Be(salesOrderNumber);
            poActualv2.PoItems[itemNumber - 1].SoItemNumber.Should().Be(salesOrderItem);
            poActualv2.PoItems[itemNumber - 1].Status.Should().Be(PoItemStatus.Accepted);
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
    }
}
