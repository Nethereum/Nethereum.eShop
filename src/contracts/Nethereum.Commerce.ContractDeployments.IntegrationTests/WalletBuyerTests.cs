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
using static Nethereum.Commerce.ContractDeployments.IntegrationTests.PoHelpers;
using static Nethereum.Commerce.Contracts.ContractEnums;
using Buyer = Nethereum.Commerce.Contracts.WalletBuyer.ContractDefinition;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;
using Nethereum.StandardTokenEIP20;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class WalletBuyerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        private const byte PO_ITEM_NUMBER = 1;
        private const byte PO_ITEM_INDEX = PO_ITEM_NUMBER - 1;
        private const string SALES_ORDER_NUMBER = "SalesOrder01";
        private const string SALES_ORDER_ITEM = "10";

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
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId);
            var txReceiptCreate = await _contracts.Deployment.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve PO as-built and check
            var poAsBuilt = await GetPoFromBuyerContractAndDisplayAsync(poNumberAsBuilt);
            CheckCreatedPoFieldsMatch(poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(), poNumberAsBuilt);
        }

        [Fact]
        public async void ShouldGetPoBySellerAndQuote()
        {
            // Prepare a new PO and create it
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId);
            var txReceiptCreate = await _contracts.Deployment.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve PO as-built using Seller and Quote, and check
            var poAsBuilt = (await _contracts.Deployment.WalletBuyerService.GetPoBySellerAndQuoteQueryAsync(poAsRequested.SellerId, quoteId)).Po;
            CheckCreatedPoFieldsMatch(poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(), poNumberAsBuilt);
        }

        [Fact]
        public async void ShouldCreateNewPoAndTransferFunds()
        {
            // Prepare a new PO
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId);

            //----------------------------------------------------------
            // BEFORE PO RAISED
            //----------------------------------------------------------
            // Balance of Web3, before test starts (check account running these tests has enough funds to pay for the PO)
            StandardTokenService sts = new StandardTokenService(_contracts.Web3, poAsRequested.CurrencyAddress);
            var totalPoValue = poAsRequested.GetTotalCurrencyValue();
            var web3AddressBalance = await sts.BalanceOfQueryAsync(_contracts.Web3.TransactionManager.Account.Address);
            web3AddressBalance.Should().BeGreaterOrEqualTo(totalPoValue, "the Web3 account must be able to pay for whole PO");
            _output.WriteLine($"PO: {poAsRequested.PoNumber}  total value {await totalPoValue.PrettifyAsync(sts)}");

            // Balance of WalletBuyer, before test starts
            var walletBuyerBalance = await sts.BalanceOfQueryAsync(poAsRequested.BuyerWalletAddress);
            _output.WriteLine($"Wallet Buyer balance before test: {await walletBuyerBalance.PrettifyAsync(sts)}");

            // Transfer required funds from current Web3 acccount to wallet buyer
            var txTransfer = await sts.TransferRequestAndWaitForReceiptAsync(poAsRequested.BuyerWalletAddress, totalPoValue);
            txTransfer.Status.Value.Should().Be(1);

            // Balance of WalletBuyer, before PO raised   
            walletBuyerBalance = await sts.BalanceOfQueryAsync(poAsRequested.BuyerWalletAddress);
            _output.WriteLine($"Wallet Buyer balance after receiving funding from Web3 account: {await walletBuyerBalance.PrettifyAsync(sts)}");

            // Balance of Funding, before PO raised   
            var fundingBalanceBefore = await sts.BalanceOfQueryAsync(_contracts.Deployment.FundingService.ContractHandler.ContractAddress);
            _output.WriteLine($"Funding balance before PO: {await fundingBalanceBefore.PrettifyAsync(sts)}");

            //----------------------------------------------------------
            // RAISE PO 
            //----------------------------------------------------------
            // Create PO on-chain
            // NB this approves token transfer from WALLET BUYER contract (NOT msg.sender == current web3 account) to FUNDING contract
            var txReceiptCreate = await _contracts.Deployment.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceiptCreate.Status.Value.Should().Be(1);
            _output.WriteLine($"... PO created ...");

            //----------------------------------------------------------
            // AFTER PO RAISED
            //----------------------------------------------------------
            // Balance of WalletBuyer, after PO raised   
            walletBuyerBalance = await sts.BalanceOfQueryAsync(poAsRequested.BuyerWalletAddress);
            _output.WriteLine($"Wallet Buyer balance after PO: {await walletBuyerBalance.PrettifyAsync(sts)}");

            // Balance of Funding, after PO raised   
            var fundingBalanceAfter = await sts.BalanceOfQueryAsync(_contracts.Deployment.FundingService.ContractHandler.ContractAddress);
            _output.WriteLine($"Funding balance after PO: {await fundingBalanceAfter.PrettifyAsync(sts)}");

            // Check
            var diff = fundingBalanceAfter - fundingBalanceBefore;
            diff.Should().Be(totalPoValue, "funding contract should have increased in value by the PO value");
        }

        [Fact]
        public async void ShouldCreateNewPoAndRetrieveIt()
        {
            // Prepare a new PO
            uint quoteId = GetRandomInt();
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId);

            // Request creation of new PO
            var txReceipt = await _contracts.Deployment.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceipt.Status.Value.Should().Be(1);

            // Check PO create events
            var logPoCreateRequest = txReceipt.DecodeAllEvents<PurchaseOrderCreateRequestLogEventDTO>().FirstOrDefault();
            logPoCreateRequest.Should().NotBeNull();  // <= PO as requested
            var logPoCreated = txReceipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();        // <= PO as built
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve the as-built PO 
            var poAsBuilt = (await _contracts.Deployment.WalletBuyerService.GetPoQueryAsync(poNumberAsBuilt)).Po;

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
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId);

            // Request creation of new PO
            var txReceipt = await _contracts.Deployment.WalletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested);
            txReceipt.Status.Value.Should().Be(1);

            // Check PO create events
            var logPoCreateRequest = txReceipt.DecodeAllEvents<PurchaseOrderCreateRequestLogEventDTO>().FirstOrDefault();
            logPoCreateRequest.Should().NotBeNull();  // <= PO as requested
            var logPoCreated = txReceipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();        // <= PO as built
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve the as-built PO 
            var poAsBuilt = (await _contracts.Deployment.WalletBuyerService.GetPoBySellerAndQuoteQueryAsync(poAsRequested.SellerId, poAsRequested.QuoteId)).Po;

            // Most fields should be the same between poAsRequested and poAsBuilt (contract adds some fields to the poAsBuilt, e.g. it assigns the poNumber)
            CheckCreatedPoFieldsMatch(poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(), poNumberAsBuilt);

            // Info
            DisplayPoHeader(_output, poAsBuilt.ToStoragePo());
        }

        private async Task<Buyer.Po> CreateBuyerPoAsync(uint quoteId)
        {
            return CreatePoForPurchasingContract(
                buyerAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                receiverAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerWalletAddress: _contracts.Deployment.WalletBuyerService.ContractHandler.ContractAddress.ToLowerInvariant(),
                currencySymbol: await _contracts.Deployment.MockDaiService.SymbolQueryAsync(),
                currencyAddress: _contracts.Deployment.MockDaiService.ContractHandler.ContractAddress.ToLowerInvariant(),
                quoteId).ToBuyerPo();
        }

        private async Task<Buyer.Po> GetPoFromBuyerContractAndDisplayAsync(BigInteger poNumber, string title = "PO")
        {
            var po = (await _contracts.Deployment.WalletBuyerService.GetPoQueryAsync(poNumber)).Po;
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
