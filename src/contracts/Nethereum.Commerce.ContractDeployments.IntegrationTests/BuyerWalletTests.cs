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
using Nethereum.ABI.FunctionEncoding;
using System.Collections.Generic;

namespace Nethereum.Commerce.ContractDeployments.IntegrationTests
{
    [Collection("Contract Deployment Collection")]
    public class BuyerWalletTests
    {
        private readonly ITestOutputHelper _output;
        private readonly ContractDeploymentsFixture _contracts;

        private const byte PO_ITEM_NUMBER = 1;
        private const byte PO_ITEM_INDEX = PO_ITEM_NUMBER - 1;
        private const string SALES_ORDER_NUMBER = "SalesOrder01";
        private const string SALES_ORDER_ITEM = "10";

        public BuyerWalletTests(ContractDeploymentsFixture fixture, ITestOutputHelper output)
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
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve PO as-built and check
            var poAsBuilt = await GetPoFromBuyerContractAndDisplayAsync(poAsRequested.EShopId, poNumberAsBuilt);
            CheckCreatedPoFieldsMatch(poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(), poNumberAsBuilt);
        }

        [Fact]
        public async void ShouldGetPoBySellerAndQuote()
        {
            // Prepare a new PO and create it            
            var quoteId = GetRandomInt();
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId);
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Get the PO number that was assigned
            var logPoCreated = txReceiptCreate.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve PO as-built using Seller and Quote, and check
            var poAsBuilt = (await _contracts.Deployment.BuyerWalletService.GetPoByEshopIdAndQuoteQueryAsync(
                poAsRequested.EShopId, quoteId)).Po;
            CheckCreatedPoFieldsMatch(poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(), poNumberAsBuilt);
        }

        [Fact]
        public async void ShouldCreatePoAndTransferFunds()
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
            var fundingBalanceBefore = await sts.BalanceOfQueryAsync(_contracts.Deployment.FundingService.ContractHandler.ContractAddress);
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
            var fundingBalanceAfter = await sts.BalanceOfQueryAsync(_contracts.Deployment.FundingService.ContractHandler.ContractAddress);
            _output.WriteLine($"Funding balance after PO: {await fundingBalanceAfter.PrettifyAsync(sts)}");

            // Check
            var diff = fundingBalanceAfter - fundingBalanceBefore;
            diff.Should().Be(totalPoValue, "funding contract should have increased in value by the PO value");
        }

        [Fact]
        public async void ShouldCreatePoAndRetrieveIt()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceipt = await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceipt.Status.Value.Should().Be(1);

            // Check PO create events
            var logPoCreateRequest = txReceipt.DecodeAllEvents<PurchaseOrderCreateRequestLogEventDTO>().FirstOrDefault();
            logPoCreateRequest.Should().NotBeNull();  // <= PO as requested
            var logPoCreated = txReceipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();        // <= PO as built
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve the as-built PO 
            var poAsBuilt = (await _contracts.Deployment.BuyerWalletService.GetPoQueryAsync(
                poAsRequested.EShopId, poNumberAsBuilt)).Po;

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
        public async void ShouldFailToCreatePoWithoutFunding()
        {
            // Prepare a new PO, notice it is created with a large value, in case of leftover tokens transferred from other tests            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt(), isLargeValue: true);
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            // DONT send any funds, so BuyerWallet has insufficient funds and creation should fail
            Func<Task> act = async () => await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            act.Should().Throw<SmartContractRevertException>(); // exception thrown by token, so can't know what actual message will be                        
        }

        [Fact]
        public async void ShouldFailToCreatePoWhenQuoteHasExpired()
        {
            // Prepare a new PO with quote expiry date to be expired
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            poAsRequested.QuoteExpiryDate = new BigInteger(DateTimeOffset.Now.ToUnixTimeSeconds() - 3600); // quote expired an hour ago
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);

            // Attempt to create PO, it should fail
            Func<Task> act = async () => await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            act.Should().Throw<SmartContractRevertException>().WithMessage(QUOTE_EXCEPTION_EXPIRY_PASSED);
        }

        [Fact]
        public async void ShouldFailToCreatePoWhenEshopDoesNotExist()
        {
            // Prepare a new PO with a non-existent eShop
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            poAsRequested.EShopId = GetRandomString();
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);

            // Attempt to create PO, it should fail
            Func<Task> act = async () => await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            act.Should().Throw<SmartContractRevertException>().WithMessage(PO_EXCEPTION_ESHOP_NO_PURCH_ADD);
        }

        [Fact]
        public async void ShouldFailToCreatePoWhenEshopOrSellerIsInactive()
        {
            // Prepare a new PO
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);

            // Temporarily make eShop inactive
            var eShop = (await _contracts.Deployment.BusinessPartnerStorageService.GetEshopQueryAsync(poAsRequested.EShopId)).EShop;
            eShop.Should().NotBeNull();
            eShop.IsActive = false;
            var eShopSetTx = await _contracts.Deployment.BusinessPartnerStorageService.SetEshopRequestAndWaitForReceiptAsync(eShop);
            eShopSetTx.Status.Value.Should().Be(1);

            // Attempt to create PO, it should fail
            Func<Task> act = async () => await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            act.Should().Throw<SmartContractRevertException>().WithMessage(PO_EXCEPTION_ESHOP_INACTIVE);

            // Make eShop active again, else we will mess up other tests
            eShop.IsActive = true;
            eShopSetTx = await _contracts.Deployment.BusinessPartnerStorageService.SetEshopRequestAndWaitForReceiptAsync(eShop);
            eShopSetTx.Status.Value.Should().Be(1);

            // Temporarily make Seller inactive
            var seller = (await _contracts.Deployment.BusinessPartnerStorageService.GetSellerQueryAsync(poAsRequested.SellerId)).Seller;
            seller.Should().NotBeNull();
            seller.IsActive = false;
            var sellerSetTx = await _contracts.Deployment.BusinessPartnerStorageService.SetSellerRequestAndWaitForReceiptAsync(seller);
            sellerSetTx.Status.Value.Should().Be(1);

            // Attempt to create PO, it should fail
            act = async () => await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            act.Should().Throw<SmartContractRevertException>().WithMessage(PO_EXCEPTION_SELLER_INACTIVE);

            // Make seller active again, else we mess up other tests
            seller.IsActive = true;
            sellerSetTx = await _contracts.Deployment.BusinessPartnerStorageService.SetSellerRequestAndWaitForReceiptAsync(seller);
            sellerSetTx.Status.Value.Should().Be(1);
        }

        [Fact]
        public async void ShouldFailToCreatePoWithoutCorrectSigner()
        {
            // Prepare a new PO            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            // DONT use a valid address to sign it, creation should fail. Sign it with the 2ndry user, 
            // whereas the eshop is configured to have at least main Web3 user as signer.
            // See BusinessPartnerStorage.sol master data creation in:
            //   Nethereum.eShop\src\contracts\Nethereum.Commerce.Contracts\Deployment\ContractDeployment.cs
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3SecondaryUser);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            Func<Task> act = async () => await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            act.Should().Throw<SmartContractRevertException>().WithMessage(QUOTE_EXCEPTION_WRONG_SIG);
        }

        [Fact]
        public async void ShouldFailToCreatePoWhenQuoteAlreadyUsed()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceiptCreate = await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceiptCreate.Status.Value.Should().Be(1);

            // Attempt to create PO using the same quote again, it should fail
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            Func<Task> act = async () => await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            act.Should().Throw<SmartContractRevertException>().WithMessage(QUOTE_EXCEPTION_QUOTE_IN_USE);
        }

        [Fact]
        public async void ShouldCreatePoAndRetrieveItBySellerAndQuote()
        {
            // Prepare a new PO and create it            
            Buyer.Po poAsRequested = await CreateBuyerPoAsync(quoteId: GetRandomInt());
            var signature = poAsRequested.GetSignatureBytes(_contracts.Web3);
            await PrepSendFundsToBuyerWalletForPo(_contracts.Web3, poAsRequested);
            var txReceipt = await _contracts.Deployment.BuyerWalletService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poAsRequested, signature);
            txReceipt.Status.Value.Should().Be(1);

            // Check PO create events
            var logPoCreateRequest = txReceipt.DecodeAllEvents<PurchaseOrderCreateRequestLogEventDTO>().FirstOrDefault();
            logPoCreateRequest.Should().NotBeNull();  // <= PO as requested
            var logPoCreated = txReceipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();
            logPoCreated.Should().NotBeNull();        // <= PO as built
            var poNumberAsBuilt = logPoCreated.Event.Po.PoNumber;

            // Retrieve the as-built PO 
            var poAsBuilt = (await _contracts.Deployment.BuyerWalletService.GetPoByEshopIdAndQuoteQueryAsync(
                poAsRequested.EShopId, poAsRequested.QuoteId)).Po;

            // Most fields should be the same between poAsRequested and poAsBuilt (contract adds some fields to the poAsBuilt, e.g. it assigns the poNumber)
            CheckCreatedPoFieldsMatch(poAsRequested.ToStoragePo(), poAsBuilt.ToStoragePo(), poNumberAsBuilt);

            // Info
            DisplayPoHeader(_output, poAsBuilt.ToStoragePo());
        }

        private async Task<Buyer.Po> CreateBuyerPoAsync(uint quoteId, bool isLargeValue = false)
        {
            return CreatePoForPurchasingContracts(
                buyerUserAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerReceiverAddress: _contracts.Web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerWalletAddress: _contracts.Deployment.BuyerWalletService.ContractHandler.ContractAddress.ToLowerInvariant(),
                eShopId: _contracts.Deployment.ContractNewDeploymentConfig.Eshop.EShopId,
                sellerId: _contracts.Deployment.ContractNewDeploymentConfig.Seller.SellerId,
                currencySymbol: await _contracts.Deployment.MockDaiService.SymbolQueryAsync(),
                currencyAddress: _contracts.Deployment.MockDaiService.ContractHandler.ContractAddress.ToLowerInvariant(),
                quoteId,
                isLargeValue).ToBuyerPo();
        }

        private async Task<Buyer.Po> GetPoFromBuyerContractAndDisplayAsync(string eShopId, BigInteger poNumber, string title = "PO")
        {
            var po = (await _contracts.Deployment.BuyerWalletService.GetPoQueryAsync(eShopId, poNumber)).Po;
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
