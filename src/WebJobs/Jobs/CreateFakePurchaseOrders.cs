using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.BuyerWallet;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.eShop.ApplicationCore.Entities.ConfigurationAggregate;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using static Nethereum.Commerce.Contracts.ContractEnums;
using Storage = Nethereum.Commerce.Contracts.PoStorage.ContractDefinition;

namespace Nethereum.eShop.WebJobs.Jobs
{
    public class CreateFakePurchaseOrders : ICreateFakePurchaseOrders
    {
        private readonly IConfiguration _configuration;
        private readonly ISettingRepository _settingRepository;
        private readonly IQuoteRepository _quoteRepository;

        public CreateFakePurchaseOrders(IConfiguration configuration, ISettingRepository settingRepository, IQuoteRepository quoteRepository)
        {
            _configuration = configuration;
            _settingRepository = settingRepository;
            this._quoteRepository = quoteRepository;
        }

        public async Task ExecuteAsync(ILogger logger)
        {
            var dbBasedConfig = await _settingRepository.GetEShopConfigurationSettingsAsync().ConfigureAwait(false);

            if (!dbBasedConfig.CreateFakePurchaseOrdersJob.Enabled) return;

            var pendingQuotes = await _quoteRepository.GetQuotesRequiringPurchaseOrderAsync();

            logger.LogInformation($"{pendingQuotes.Count} were found requiring a purchase order");

            if (!pendingQuotes.Any()) return;

            var url = _configuration["EthereumRpcUrl"];
            var privateKey = _configuration["AccountPrivateKey"];

            var web3 = new Web3.Web3(new Account(privateKey), url);

            var walletBuyerService = new BuyerWalletService(web3, dbBasedConfig.BuyerWalletAddress);

            foreach (var quote in pendingQuotes)
            {
                await CreatePoForQuote(dbBasedConfig, web3, logger, walletBuyerService, quote);
            }
        }

        private async Task CreatePoForQuote(EShopConfigurationSettings dbBasedConfig, Web3.Web3 web3, ILogger logger, BuyerWalletService walletBuyerService, Quote quote)
        {
            var existing = await walletBuyerService.GetPoByEshopIdAndQuoteQueryAsync(dbBasedConfig.EShop.Id, quote.Id);
            if (existing?.Po?.PoNumber > 0)
            {
                quote.PoNumber = (long)existing.Po.PoNumber;
                quote.Status = QuoteStatus.AwaitingOrder;
                _quoteRepository.Update(quote);
                await _quoteRepository.UnitOfWork.SaveEntitiesAsync();
                return;
            }

            var po = CreateDummyPoForPurchasingCreate(dbBasedConfig, web3, quote).ToBuyerPo();
            var signature = po.GetSignatureBytes(web3);
            var poArgs = new Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition.CreatePurchaseOrderFunction { Po = po, Signature = signature };
            var receipt = await walletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(poArgs);

            var createdEvent = receipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();

            if (createdEvent != null)
            {
                logger.LogInformation($"Created PO from quote.  Block Number: {receipt.BlockNumber}, PO: {createdEvent?.Event.PoNumber}");
                quote.PoNumber = (long)createdEvent.Event.PoNumber;
                quote.Status = QuoteStatus.AwaitingOrder;
                quote.TransactionHash = receipt.TransactionHash;
                _quoteRepository.Update(quote);
                await _quoteRepository.UnitOfWork.SaveEntitiesAsync();
            }
            else
            {
                logger.LogError($"PO Creation failed for quote {quote.Id}.  No created event log was found in the receipt");
            }
        }

        public Po CreateDummyPoForPurchasingCreate(EShopConfigurationSettings dbBasedConfig, Web3.Web3 web3, Quote quote)
        {
            return CreatePoForPurchasingContracts(
                buyerUserAddress: web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerReceiverAddress: web3.TransactionManager.Account.Address.ToLowerInvariant(),
                buyerWalletAddress: dbBasedConfig.BuyerWalletAddress.ToLowerInvariant(),
                eShopId: dbBasedConfig.EShop.Id,
                sellerId: dbBasedConfig.Seller.Id,
                currencySymbol: dbBasedConfig.CurrencySymbol,
                currencyAddress: dbBasedConfig.CurrencyAddress.ToLowerInvariant(),
                quote: quote,
                isLargeValue: false);
        }

        public Po CreatePoForPurchasingContracts(
            string buyerUserAddress,
            string buyerReceiverAddress,
            string buyerWalletAddress,
            string eShopId,
            string sellerId,
            string currencySymbol,
            string currencyAddress,
            Quote quote,
            bool isLargeValue = false)
        {
            BigInteger valueLine01 = BigInteger.Parse("110000000000000000000"); // eg this is 110 dai
            BigInteger valueLine02 = BigInteger.Parse("220000000000000000000"); // eg this is 220 dai
            if (isLargeValue)
            {
                valueLine01 *= 1000;
                valueLine02 *= 1000;
            }

            var items = new List<Storage.PoItem>(quote.ItemCount());

            foreach (var quoteItem in quote.QuoteItems)
            {
                items.Add(new Storage.PoItem
                {
                    // PoNumber assigned by contract
                    // PoItemNumber assigned by contract
                    SoNumber = string.Empty,
                    SoItemNumber = string.Empty,
                    ProductId = quoteItem.ItemOrdered.Gtin,
                    Quantity = quoteItem.Quantity,
                    Unit = "EA",
                    QuantitySymbol = "NA",
                    // TODO: remove or replace Quantity Address
                    QuantityAddress = "0x40ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610".ToLowerInvariant(),
                    CurrencyValue = quoteItem.CurrencyValue == null ? 0 : BigInteger.Parse(quoteItem.CurrencyValue),
                    // Status assigned by contract
                    // GoodsIssuedDate assigned by contract
                    // GoodsReceivedDate assigned by contract
                    // PlannedEscrowReleaseDate assigned by contract
                    // ActualEscrowReleaseDate assigned by contract
                    // IsEscrowReleased assigned by contract
                    // CancelStatus assigned by contract
                });
            }

            var po = new Storage.Po()
            {
                // PoNumber assigned by contract

                BuyerUserAddress = buyerUserAddress,
                BuyerReceiverAddress = buyerReceiverAddress,
                BuyerWalletAddress = buyerWalletAddress,

                EShopId = eShopId,
                QuoteId = quote.Id,
                QuoteExpiryDate = new BigInteger(DateTimeOffset.Now.AddHours(1).ToUnixTimeSeconds()),
                QuoteSignerAddress = string.Empty,  // assigned by contract

                SellerId = sellerId,

                CurrencySymbol = currencySymbol,
                CurrencyAddress = currencyAddress,
                PoType = PoType.Cash,
                // PoCreateDate assigned by contract
                // PoItemCount assigned by contract                
                PoItems = items,
                // RulesCount assigned by contract
                Rules = new List<byte[]>()
                {
                    "rule01".ConvertToBytes32(),
                    "rule02".ConvertToBytes32(),
                    "rule03".ConvertToBytes32()
                }
            };

            return po.ToPurchasingPo();

        }
    }
}
