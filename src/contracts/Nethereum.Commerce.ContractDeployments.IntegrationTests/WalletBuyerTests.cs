using FluentAssertions;
using Nethereum.Commerce.ContractDeployments.IntegrationTests.Config;
using Nethereum.Commerce.Contracts;
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
            uint poNumber = GetRandomInt();
            string approverAddress = "0x38ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610";
            uint quoteId = GetRandomInt();
            Storage.Po poExpectedStorage = CreateTestPo(poNumber, approverAddress, quoteId);
            Buyer.Po poExpectedBuyer = poExpectedStorage.ToBuyerPo();

            // Create new PO
            var txReceipt = await _contracts.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poExpectedBuyer);
            txReceipt.Status.Value.Should().Be(1);

            // Retrieve PO 
            var poActualBuyer = (await _contracts.WalletBuyerService.GetPoQueryAsync(poNumber)).Po;

            // They should be the same
            CheckEveryPoFieldMatches(poExpectedBuyer, poActualBuyer);
        }

        [Fact]
        public async void ShouldCreateNewPoAndRetrieveItBySellerAndQuote()
        {

        }
    }
}
