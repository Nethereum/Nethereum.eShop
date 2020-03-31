using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.BuyerWallet;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.WebJobs.Configuration;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using static Nethereum.Commerce.Contracts.ContractEnums;

namespace Nethereum.eShop.WebJobs.Jobs
{
    public class CreateFakePurchaseOrders : ICreateFakePurchaseOrders
    {
        private readonly EshopConfiguration _config;
        private readonly IQuoteRepository _quoteRepository;

        public CreateFakePurchaseOrders(EshopConfiguration config, IQuoteRepository quoteRepository)
        {
            this._config = config;
            this._quoteRepository = quoteRepository;
        }

        public async Task ExecuteAsync(ILogger logger)
        {
            if (!_config.CreateFakePurchaseOrders) return;

            var pendingQuotes = await _quoteRepository.GetQuotesRequiringPurchaseOrderAsync();

            logger.LogInformation($"{pendingQuotes.Count} were found requiring a purchase order");

            if (!pendingQuotes.Any()) return;

            var account = new Account(_config.AccountPrivateKey);
            var web3 = new Web3.Web3(account);
            var walletBuyerService = new BuyerWalletService(web3, _config.BuyerWalletAddress);

            foreach (var quote in pendingQuotes)
            {
                await CreatePoForQuote(logger, walletBuyerService, quote);
            }
        }

        private async Task CreatePoForQuote(ILogger logger, BuyerWalletService walletBuyerService, Quote quote)
        {
            var existing = await walletBuyerService.GetPoQueryAsync(_config.EShopId, quote.Id);
            if (existing?.Po?.PoNumber > 0)
            {
                quote.PoNumber = (long)existing.Po.PoNumber;
                quote.Status = QuoteStatus.AwaitingOrder;
                _quoteRepository.Update(quote);
                await _quoteRepository.UnitOfWork.SaveEntitiesAsync();
                return;
            }

            var po = CreateDummyPoForPurchasingCreate(quote, walletBuyerService.ContractHandler.ContractAddress).ToBuyerPo();
            var poArgs = new Nethereum.Commerce.Contracts.BuyerWallet.ContractDefinition.CreatePurchaseOrderFunction { Po = po };
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

        public Po CreateDummyPoForPurchasingCreate(Quote quote, string buyerWalletAddress)
        {
            var po = new Po()
            {
                // PoNumber assigned by contract
                BuyerWalletAddress = buyerWalletAddress,
                CurrencySymbol = "DAI",
                CurrencyAddress = "0xef76bcb4216fbbbd4d6e88082d5654def9b6fe2f",
                QuoteId = quote.Id,
                QuoteExpiryDate = DateTimeOffset.UtcNow.AddMonths(1).ToUnixTimeSeconds(),
                PoType = PoType.Cash,
                SellerId = _config.EShopId,
                // PoCreateDate assigned by contract
                // PoItemCount assigned by contract
                PoItems = new List<PoItem>()
            };

            //gtin1111

            foreach (var quoteItem in quote.QuoteItems)
            {
                po.PoItems.Add(new PoItem
                {
                    // PoNumber assigned by contract
                    // PoItemNumber assigned by contract
                    SoNumber = string.Empty,
                    SoItemNumber = string.Empty,
                    ProductId = quoteItem.ItemOrdered.Gtin,
                    Quantity = quoteItem.Quantity,
                    Unit = "EA",
                    QuantitySymbol = "NA",
                    QuantityAddress = "0x40ed4f49ec2c7bdcce8631b1a7b54ed5d4aa9610",
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

            return po;
        }
    }
}
