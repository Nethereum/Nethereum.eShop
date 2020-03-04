using Microsoft.Extensions.Logging;
using Nethereum.Commerce.Contracts;
using Nethereum.Commerce.Contracts.Purchasing.ContractDefinition;
using Nethereum.Commerce.Contracts.WalletBuyer;
using Nethereum.Contracts;
using Nethereum.eShop.ApplicationCore.Entities.QuoteAggregate;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Specifications;
using Nethereum.eShop.WebJobs.Configuration;
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
        private readonly EshopConfiguration _config;
        private readonly IQuoteRepository _quoteRepository;

        public CreateFakePurchaseOrders(EshopConfiguration config, IQuoteRepository quoteRepository)
        {
            this._config = config;
            this._quoteRepository = quoteRepository;
        }

        public class GetQuotesRequiringPurchaseOrderSpec : BaseSpecification<Quote>
        {
            public GetQuotesRequiringPurchaseOrderSpec()
                : base(quote => quote.Status == QuoteStatus.Pending && quote.PoNumber == null)
            {
                AddInclude(b => b.QuoteItems);
            }
        }

        public async Task ExecuteAsync(ILogger logger)
        {
            if (!_config.CreateFakePurchaseOrders) return;

            var pendingQuotes = await _quoteRepository.ListAsync(new GetQuotesRequiringPurchaseOrderSpec());

            logger.LogInformation($"{pendingQuotes.Count} were found requiring a purchase order");

            if (!pendingQuotes.Any()) return;

            var account = new Account(_config.AccountPrivateKey);
            var web3 = new Web3.Web3(account);
            var walletBuyerService = new WalletBuyerService(web3, _config.BuyerWalletAddress);

            foreach (var quote in pendingQuotes)
            {
                await CreatePoForQuote(logger, walletBuyerService, quote);
            }
        }

        private async Task CreatePoForQuote(ILogger logger, WalletBuyerService walletBuyerService, Quote quote)
        {
            var existing = await walletBuyerService.GetPoBySellerAndQuoteQueryAsync(_config.SellerId, quote.Id);
            if (existing?.Po?.PoNumber > 0)
            {
                quote.PoNumber = (long)existing.Po.PoNumber;
                quote.Status = QuoteStatus.AwaitingOrder;
                await _quoteRepository.UpdateAsync(quote);
                return;
            }

            var po = CreateDummyPoForPurchasingCreate(quote, walletBuyerService.ContractHandler.ContractAddress).ToBuyerPo();
            var receipt = await walletBuyerService.CreatePurchaseOrderRequestAndWaitForReceiptAsync(po);

            var createdEvent = receipt.DecodeAllEvents<PurchaseOrderCreatedLogEventDTO>().FirstOrDefault();

            if (createdEvent != null)
            {
                logger.LogInformation($"Created PO from quote.  Block Number: {receipt.BlockNumber}, PO: {createdEvent?.Event.PoNumber}");
                quote.PoNumber = (long)createdEvent.Event.PoNumber;
                quote.Status = QuoteStatus.AwaitingOrder;
                quote.TransactionHash = receipt.TransactionHash;
                await _quoteRepository.UpdateAsync(quote);
            }
            else
            {
                logger.LogError($"PO Creation failed for quote {quote.Id}.  No created event log was found in the receipt");
            }
        }

        public Storage.Po CreateDummyPoForPurchasingCreate(Quote quote, string buyerWalletAddress)
        {
            var po = new Storage.Po()
            {
                // PoNumber assigned by contract
                BuyerAddress = "0x94618601fe6cb8912b274e5a00453949a57f8c1e",
                ReceiverAddress = "0x94618601fe6cb8912b274e5a00453949a57f8c1e",
                BuyerWalletAddress = buyerWalletAddress,
                CurrencySymbol = "DAI",
                CurrencyAddress = "0xef76bcb4216fbbbd4d6e88082d5654def9b6fe2f",
                QuoteId = quote.Id,
                QuoteExpiryDate = DateTimeOffset.UtcNow.AddMonths(1).ToUnixTimeSeconds(),
                ApproverAddress = string.Empty,  // assigned by contract
                PoType = PoType.Cash,
                SellerId = _config.SellerId,
                // PoCreateDate assigned by contract
                // PoItemCount assigned by contract
                PoItems = new List<Storage.PoItem>()
            };

            //gtin1111

            foreach (var quoteItem in quote.QuoteItems)
            {
                po.PoItems.Add(new Storage.PoItem
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
