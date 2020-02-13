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

        //[Fact(Skip = "Not working yet")]
        [Fact]
        public async void ShouldSetPoStatusToAccepted()
        {
            // Prepare a new PO
            uint poNumber = 0; //assigned by contract
            string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPo(poNumber, approverAddress, quoteId).ToBuyerPo();

            // Request creation of new PO using the Buyer wallet
            var txReceiptCreate = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;
            _output.WriteLine($"{poNumberAsBuilt}");

            // Accept one of the PO items
            byte itemIndex = 1;
            var txReceiptAccept = await _contracts.WalletSellerService.SetPoItemAcceptedRequestAndWaitForReceiptAsync(
                poNumberAsBuilt, itemIndex, "KevinSalesOrder", "10");
            txReceiptAccept.Status.Value.Should().Be(1);

            // Examine log
            var logPoAccepted = txReceiptAccept.DecodeAllEvents<PurchaseItemAcceptedLogEventDTO>().FirstOrDefault();
            logPoAccepted.Should().NotBeNull();
            logPoAccepted.Event.PoItem.Status.Should().Be(PoItemStatus.Accepted);
            DisplayPoItem(_output, logPoAccepted.Event.PoItem.ToStoragePoItem());

            _output.WriteLine($"Log PO number : {logPoAccepted.Event.PoNumber}");
            //

            // Retrieve the PO 
            var poActual = (await _contracts.WalletSellerService.GetPoQueryAsync(poNumberAsBuilt)).Po;

            // Check it has been updated correctly
            poActual.PoItems[itemIndex].SoNumber.Should().Be("KevinSalesOrder");
            poActual.PoItems[itemIndex].Status.Should().Be(PoItemStatus.Accepted);
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
