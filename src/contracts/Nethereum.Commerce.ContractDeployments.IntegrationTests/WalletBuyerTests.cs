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

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class WalletBuyerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        public WalletBuyerTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
        {
            // ContractDeploymentsFixture performed a complete deployment.
            // See Output window -> Tests for deployment logs.
            _contracts = fixture;
            _output = output;
        }

        [Fact]
        public async void ShouldGetPo()
        {
            // Get test PO created during fixture creation
            var poExpected = _contracts.PoTest;
            var poActual = (await _contracts.WalletBuyerService.GetPoQueryAsync(poExpected.PoNumber)).Po;
            CheckEveryPoFieldMatches(poExpected.ToStoragePo(), poActual.ToStoragePo());
        }

        [Fact]
        public async void ShouldGetPoBySellerAndQuote()
        {
            // Get test PO created during fixture creation
            var sellerId = _contracts.PoTest.SellerId;
            var quoteId = _contracts.PoTest.QuoteId;
            var poExpected = _contracts.PoTest;
            var poActual = (await _contracts.WalletBuyerService.GetPoBySellerAndQuoteQueryAsync(sellerId, quoteId)).Po;
            CheckEveryPoFieldMatches(poExpected.ToStoragePo(), poActual.ToStoragePo());
        }

        [Fact]
        public async void ShouldCreateNewPoAndRetrieveIt()
        {
            // Prepare a new PO
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();

            // Request creation of new PO
            var txReceipt = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceipt.Status.Value.Should().Be(1);

            // Check PO create events
            var logPoCreateRequest = txReceipt.DecodeAllEvents<PurchaseOrderCreateRequestLogEventDTO>().FirstOrDefault();
            logPoCreateRequest.Should().NotBeNull();  // <= PO as requested
            var logPoCreated = txReceipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();        // <= PO as built
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve the as-built PO 
            var poAsBuilt = (await _contracts.WalletBuyerService.GetPoQueryAsync(poNumberAsBuilt)).Po;

            // Most fields should be the same between poAsRequested and poAsBuilt (contract adds some fields to the poAsBuilt, e.g. it assigns the poNumber)
            var block = await _contracts.Web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(txReceipt.BlockNumber);
            var blockTimestamp = block.Timestamp.Value;
            CheckCreatedPoFieldsMatch(
                poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(),
                poNumberAsBuilt, null, blockTimestamp);

            // Info
            DisplayPoHeader(_output, poAsBuilt.ToStoragePo());
        }

        [Fact]
        public async void ShouldCreateNewPoAndRetrieveItBySellerAndQuote()
        {
            // Prepare a new PO
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateDummyPoForPurchasingCreate(quoteId).ToBuyerPo();
            
            // Request creation of new PO
            var txReceipt = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceipt.Status.Value.Should().Be(1);

            // Check PO create events
            var logPoCreateRequest = txReceipt.DecodeAllEvents<PurchaseOrderCreateRequestLogEventDTO>().FirstOrDefault();
            logPoCreateRequest.Should().NotBeNull();  // <= PO as requested
            var logPoCreated = txReceipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();        // <= PO as built
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve the as-built PO 
            var poAsBuilt = (await _contracts.WalletBuyerService.GetPoBySellerAndQuoteQueryAsync(poAsRequested.SellerId, poAsRequested.QuoteId)).Po;

            // Most fields should be the same between poAsRequested and poAsBuilt (contract adds some fields to the poAsBuilt, e.g. it assigns the poNumber)
            CheckCreatedPoFieldsMatch(poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(), poNumberAsBuilt);

            // Info
            DisplayPoHeader(_output, poAsBuilt.ToStoragePo());
        }
    }
}
