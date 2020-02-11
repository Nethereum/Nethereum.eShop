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
            CheckEveryPoFieldMatches(poExpected, poActual);
        }

        [Fact]
        public async void ShouldGetPoBySellerAndQuote()
        {
            // Get test PO created during fixture creation
            var sellerId = _contracts.PoTest.SellerId;
            var quoteId = _contracts.PoTest.QuoteId;
            var poExpected = _contracts.PoTest;
            var poActual = (await _contracts.WalletBuyerService.GetPoBySellerAndQuoteQueryAsync(sellerId, quoteId)).Po;
            CheckEveryPoFieldMatches(poExpected, poActual);
        }

        [Fact]
        public async void ShouldCreateNewPoAndRetrieveIt()
        {
            // Prepare a new PO
            uint poNumber = 0; //assigned by contract
            string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = CreateTestPo(poNumber, approverAddress, quoteId).ToBuyerPo();

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

            // Most, but not all fields should be the same (because contract adds some)
            poAsBuilt.PoNumber.Should().Be(poNumberAsBuilt);
            poAsBuilt.BuyerAddress.Should().Be(poAsRequested.BuyerAddress);
            poAsBuilt.ReceiverAddress.Should().Be(poAsRequested.ReceiverAddress);
            poAsBuilt.BuyerWalletAddress.Should().Be(poAsRequested.BuyerWalletAddress);
            poAsBuilt.CurrencySymbol.Should().Be(poAsRequested.CurrencySymbol);
            poAsBuilt.CurrencyAddress.Should().Be(poAsRequested.CurrencyAddress);
            poAsBuilt.QuoteId.Should().Be(poAsRequested.QuoteId);

        }

        [Fact]
        public async void ShouldCreateNewPoAndRetrieveItBySellerAndQuote()
        {

        }
    }
}
